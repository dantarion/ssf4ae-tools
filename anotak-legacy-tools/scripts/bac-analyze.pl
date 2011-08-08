#/usr/bin/perl -w
# by anotak
use strict;
use warnings;
if(defined($ARGV[0])) {
	print "ok good\n";
} else {
	print "not enough params\n";
	exit 0;
}
print "----------------------------------------------------------------------\n";
open(IN,$ARGV[0]);
binmode(IN);

# debugging mainly
sub printadd {
	my $a = shift;
	return $a . " - " . sprintf("%x",$a);
}

# loc, amount, buffer
sub sread {
	# bad programming here but whatev
	seek(IN, $_[0], 0) or die "seek:$!";
	if($_[1] < 0)
	{
		print $_[1] . " is negative amount!! \n";
	}

	read(IN, $_[2], $_[1]);
}

# endian stuff
sub convert {
	return unpack("V", shift);
}

# endian stuff
sub sconvert {
	return unpack("C", shift);
}

# (start, end). returns array.
sub slurptable {
	my ($s, $e) = (shift,shift);
	my $buffer = "";
	my @out;
	my $i = 0;
	
	while($s < $e) {
		sread($s, 4, $buffer);
		$buffer = convert($buffer);
		if($buffer != 0) {
			$out[$i] = $buffer;
			$i++;
		}
		$s += 4;
	}
	return @out;
}

my $buffer = "";
my $pointer = 0;

#sread(0x48, 4, $buffer);

#my $bac = convert($buffer);
my $bac = 0;
#print printadd($bac) . " - header BAC\n";

sread($bac, 256, $buffer);

#print $buffer . "\n";

$bac += rindex($buffer, "#BAC");
print printadd($bac) . " - actual BAC\n"; # 779040

# get animation table
my $ssf_top_table = 20;
sread($bac + $ssf_top_table, 4, $buffer);
$pointer = convert($buffer);
my $addanimation = $pointer+$bac;

print printadd($bac + $ssf_top_table) . " - pointer to animation table\n    contents - " . printadd($pointer) . " or " .  printadd($addanimation) . "\n";
# wtf is at address 24
# get end of animation table
$ssf_top_table += 8;
sread($bac + $ssf_top_table, 4, $buffer);
$pointer = convert($buffer);
my $addname = $pointer+$bac;
print printadd($bac + $ssf_top_table) . " - pointer to name table\n    contents - " . printadd($pointer) . " or " .  printadd($addname) . "\n";

$ssf_top_table += 4;
# get hit table start (so know end of name table)
sread($bac + $ssf_top_table, 4, $buffer);
$pointer = convert($buffer);
while($pointer == 0)
{
	$ssf_top_table += 4;
	sread($bac + $ssf_top_table, 4, $buffer);
	$pointer = convert($buffer);
}
my $addhit = $pointer+$bac;
print printadd($bac + $ssf_top_table) . " - pointer to hit table\n    contents - " . printadd($pointer) . " or " .  printadd($addhit) . "\n";

my @animtable = slurptable($addanimation, $addname);
my @nametable = slurptable($addname, $addhit);

# not slurping the hit table for now except for the first value.
# get hit table start (so know end of name table)
sread($addhit, 4, $buffer);
$pointer = convert($buffer);
my $firsthit = $pointer+$bac;
print printadd($addhit) . " - hit table first value\n    contents - " . printadd($pointer) . " or " .  printadd($firsthit) . "\n";

print "------------------------------------\n";
print "length of animtable -  " . scalar(@animtable) . "\n";
print "length of nametable -  " . scalar(@nametable) . "\n";

my $length = scalar(@nametable);
push(@nametable, $firsthit - $bac); #to prevent reading past
my @names = ();

for(my $i = 0; $i < $length; $i++)
{
	#print $nametable[$i]+$bac . " , " . $nametable[$i+1] . " - " . $nametable[$i] . " , " . $buffer . "\n";
	my $read_length = $nametable[$i+1]-$nametable[$i];
	if($read_length < 0)
	{
		$read_length = 5353463434;
	}
	sread($nametable[$i]+$bac, $read_length, $buffer);
	$buffer =~ s/\x00+$//;
	push @names, $buffer;
	$buffer = $i . ". " . $buffer;
	$buffer .= " "x(30-length($buffer));
	print $buffer . printadd($animtable[$i] + $bac) . "\n";
}

print printadd($nametable[199]) . "\n";

# part 2 figuring
my @a = ();
sub contains {
	my $t = shift;
	my $k = shift;
	my @b;
	
	if(defined($a[$k]))
	{
		@b = split(",", $a[$k]);
	} else {
		return 0;
	}
	foreach(@b)
	{
		#print $_;
		if($_ eq $t)
		{
			return 1;
		}
	}
	return 0;
}


my $max_out = 0;
my $max_i = 0;
my $min_out = 1294535892385283;
my $min_i = 0;
for(my $i = 0; $i < $length - 4; $i++)
{
	#print "getting " . $i . ". " . $names[$i] . ": " . printadd($animtable[$i] + $bac) . " and ends at " . printadd($animtable[$i+1] + $bac) . "\n";
	my $read_length = $animtable[$i+1]-$animtable[$i];
	if($read_length < 0)
	{
		$read_length = 5353463434;
	}
	sread($animtable[$i]+$bac, $read_length, $buffer);
	
	my $s = "a";
	my $num = 0;
	my $k = 0;
	while($k <= length($buffer) - 2) {
		$s =  sprintf("%x",sconvert(substr($buffer, $k,1)));
		
		unless(contains($s, $k)) {
			$a[$k] .= "," . $s;
		}# else {
			#$a[$k] = $s;
		#}
		
		$k++;
	}
	if($k > $max_out)
	{
		$max_out = $k;
		$max_i = $i;
	}
	elsif($k < $min_out)
	{
		$min_out = $k;
		$min_i = $i;
	}
}

my $i = 0;
foreach(@a) {
	print $i . "-- " . $_ . "\n";
	$i++;
}
print "max length: $max_out, anim $max_i $names[$max_i] \nmin length: $min_out, anim $min_i $names[$min_i] \n";

# cleanup
close(IN);
