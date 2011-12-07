for script in bac.Scripts:
	list = []
	curFrame = 0
	speed = 1
	if(script.Name == "NONE"):
		continue
	print '<h2>%s</h2>'%script.Name
	print 'IASA at %d<br />'%script.LastCancelableFrame
	if len(script.CommandLists[4].Commands) == 0:
		continue
	for i in range(0,script.TotalFrames):
		list.append(curFrame)
		for speedCommand in script.CommandLists[4].Commands:
			if i > speedCommand.StartFrame:
				if speedCommand.Multiplier != 0:
					speed = 1.0/speedCommand.Multiplier
				else:
					speed = 0
		curFrame = curFrame+speed
	print '<table><tr><th>Script</th><th>Real</th></tr>'
	for i,v in enumerate(list):
		print '<tr><td>%d</td><td>%f</td></tr>' % (i,v)
	print '</table>'
