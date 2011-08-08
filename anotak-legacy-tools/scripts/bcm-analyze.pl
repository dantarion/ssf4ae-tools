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
	seek(IN, $_[0], 0) or die "seek: $!";
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

sub slurptabletillend {
	my ($s, $e) = (shift,shift);
	my $buffer = "";
	my @out;
	my $i = 0;
	
	while($s < $e || $e == 0) {
		sread($s, 4, $buffer);
		
		$buffer = convert($buffer);
		if($buffer > 65535)
		{
			return @out;
		} else {
			if($buffer != 0) {
				$out[$i] = $buffer;
				$i++;
			}
			$s += 4;
		}
	}
	return @out;
}

my $buffer = "";
my $pointer = 0;

my $bcm = 0;

sread($bcm, 256, $buffer);


$bcm += rindex($buffer, "#BCM");
print printadd($bcm) . " - actual BCM\n"; # 779040

# get motion list
my $ssf_top_table = 32;
sread($bcm + $ssf_top_table, 4, $buffer);
$pointer = convert($buffer);
my $add_motion_list = $pointer+$bcm;

print printadd($bcm + $ssf_top_table) . " - pointer to 196 byte size motion list\n    contents - " . printadd($pointer) . " or " .  printadd($add_motion_list) . "\n";


# get motion list
$ssf_top_table = 36;
sread($bcm + $ssf_top_table, 4, $buffer);
$pointer = convert($buffer);
my $add_motion_name_table = $pointer+$bcm;

print printadd($bcm + $ssf_top_table) . " - pointer to motion name table\n    contents - " . printadd($pointer) . " or " .  printadd($add_motion_name_table) . "\n";

$ssf_top_table = 40;
sread($bcm + $ssf_top_table, 4, $buffer);
$pointer = convert($buffer);
my $add_motion_name_table_end = $pointer+$bcm;

print printadd($bcm + $ssf_top_table) . " - pointer to end of motion name table\n    contents - " . printadd($pointer) . " or " .  printadd($add_motion_name_table_end) . "\n";

$ssf_top_table = 44;
sread($bcm + $ssf_top_table, 4, $buffer);
$pointer = convert($buffer);
my $add_input_name_table = $pointer+$bcm;

print printadd($bcm + $ssf_top_table) . " - pointer to input name table\n    contents - " . printadd($pointer) . " or " .  printadd($add_input_name_table) . "\n";

$ssf_top_table = 48;
sread($bcm + $ssf_top_table, 4, $buffer);
$pointer = convert($buffer);
my $add_cancel_table = $pointer+$bcm;

print printadd($bcm + $ssf_top_table) . " - pointer to cancel table a\n    contents - " . printadd($pointer) . " or " .  printadd($add_cancel_table) . "\n";

$ssf_top_table = 52;
sread($bcm + $ssf_top_table, 4, $buffer);
$pointer = convert($buffer);
my $add_cancel_name_table = $pointer+$bcm;

print printadd($bcm + $ssf_top_table) . " - pointer to cancel name table b\n    contents - " . printadd($pointer) . " or " .  printadd($add_cancel_name_table) . "\n";


print "---------------------\n";
print( (($add_motion_name_table - $add_motion_list) / 196 ) . " is number of motions in list! \n");
print( (($add_motion_name_table_end - $add_motion_name_table) / 4 ) . " is number of motions in name table! \n");


my @motion_name_table = slurptable($add_motion_name_table, $add_motion_name_table_end);
my @input_name_table = slurptable($add_input_name_table, $add_cancel_table);
my @cancel_table = slurptable($add_cancel_table, $add_cancel_name_table);
my @cancel_name_table = slurptable($add_cancel_name_table, $add_cancel_table + $cancel_table[1]);

print "---------------------\nMOTIONS\n---------------------\n";
my $length = scalar(@motion_name_table);
push(@motion_name_table, $input_name_table[0] - $bcm); # to prevent reading past
my @motion_names = ();

for(my $i = 0; $i < $length; $i++)
{
	#print $nametable[$i]+$bac . " , " . $nametable[$i+1] . " - " . $nametable[$i] . " , " . $buffer . "\n";
	my $read_length = $motion_name_table[$i+1]-$motion_name_table[$i];
	if($read_length < 0)
	{
		$read_length = 5353463434;
	}
	sread($motion_name_table[$i]+$bcm, $read_length, $buffer);
	$buffer =~ s/\x00+$//;
	push @motion_names, $buffer;
	$buffer = $i . ". " . $buffer;
	$buffer .= " "x(30-length($buffer));
	print $buffer . printadd($add_motion_list + (196 * $i) + $bcm) . "\n";
}

print "---------------------\nINPUTS\n---------------------\n";
$length = scalar(@input_name_table);
push(@input_name_table, $cancel_name_table[0] - $bcm); # to prevent reading past
my @input_names = ();

for(my $i = 0; $i < $length; $i++)
{
	#print $nametable[$i]+$bac . " , " . $nametable[$i+1] . " - " . $nametable[$i] . " , " . $buffer . "\n";
	my $read_length = $input_name_table[$i+1]-$input_name_table[$i];
	if($read_length < 0)
	{
		$read_length = 5353463434;
	}
	sread($input_name_table[$i]+$bcm, $read_length, $buffer);
	$buffer =~ s/\x00+$//;
	push @input_names, $buffer;
	$buffer = $i . "-" . sprintf("%x",$i) . ". " . $buffer;
	$buffer .= " "x(30-length($buffer));
	print $buffer . printadd($add_motion_name_table_end + (84 * $i) + $bcm) . "\n";
}

print "---------------------\nCANCEL LIST NAMES\n---------------------\n";
$length = scalar(@cancel_name_table);
push @cancel_name_table, 0;
my @unknown_names = ();

for(my $i = 0; $i < $length; $i++)
{
	#print $nametable[$i]+$bac . " , " . $nametable[$i+1] . " - " . $nametable[$i] . " , " . $buffer . "\n";
	my $read_length = $cancel_name_table[$i+1]-$cancel_name_table[$i];
	if($read_length < 0)
	{
		$read_length = 5353463434;
	}
	sread($cancel_name_table[$i]+$bcm, $read_length, $buffer);
	$buffer =~ s/\x00+$//;
	push @input_names, $buffer;
	$buffer = $i . "-" . sprintf("%x",$i) . ". " . $buffer;
	$buffer .= " "x(30-length($buffer));
	print $buffer . printadd($cancel_table[$i*2 + 1] + $i*8 + $bcm + $add_cancel_table) . "\n";
}

# cleanup
close(IN);


# inputs seem to be 84 bytes each