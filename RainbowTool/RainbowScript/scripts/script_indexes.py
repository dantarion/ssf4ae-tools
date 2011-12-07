print "<table>"
for i,script in enumerate(bac.Scripts):
	print "<tr><td>%d</td><td>%d</td><td>%s</td></tr>"%(i-1,script.Index,script.Name)
print "</table>"
