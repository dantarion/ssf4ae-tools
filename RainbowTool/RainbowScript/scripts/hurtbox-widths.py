TARGET = bac.Scripts[1].CommandLists[9]
print '<br /><canvas style="border:1px solid" id="%s" width="500" height="500"></canvas></br>' % charName
print '<table border="1"><tr><th>X</th><th>Y</th><th>Width</th><th>Height</th></tr>'
for hurtbox in TARGET.Commands:
	print '<tr><td>%f</td><td>%f</td><td>%f</td><td>%f</td></tr>'%(hurtbox.X,hurtbox.Y,hurtbox.Width,hurtbox.Height)
print '</table>'
for hurtbox in TARGET.Commands:
	X = hurtbox.X-(hurtbox.Width/2)
	Y = hurtbox.Y-(hurtbox.Height/2)
	print '''<script>
	  var canvas = document.getElementById("%s");
	  var context = canvas.getContext("2d");
	  context.strokeRect(%f,%f, %f, %f);
</script>''' % (charName,250+X*300,250+Y*300,hurtbox.Width*300,hurtbox.Height*300)