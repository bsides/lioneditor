import os
import sys

pspFiles = os.listdir("fftpack/unknown")

#psxFiles = os.listdir("UScompare")
psxFiles=[]
dirs = ["BATTLE", "EFFECT", "EVENT", "MAP", "MENU", "WORLD", "SOUND"]
for d in dirs:
	for l in os.listdir("JP/"+d):
		psxFiles.append(d+"/"+l)

pspDict = {}
for p in pspFiles:
	f=open("fftpack/unknown/"+p)
	d=f.read()
	f.close()
	pspDict[p] = d

for f in psxFiles:
	fp = open("JP/"+f)
	psx = fp.read()
	fp.close()
	if (len(psx) == 0):
		print "// " + f + " is 0 bytes"
		continue
	found=False
	for g in pspDict:
		if (pspDict[g] == psx):
			print 'strcpy(path[%s], "%s"); // Exact match' % (g.split(".")[-1], f)
			pspDict.pop(g)
			found=True
			break
		elif (psx == pspDict[g][:len(psx)]):
			success=True
			for i in pspDict[g][len(psx):]:
				if i != "\x00":
					success=False
					break
			if success:
				print 'strcpy(path[%s], "%s");' % (g.split(".")[-1], f)
				pspDict.pop(g)
				found=True
				break
			else:
				print 'strcpy(path[%s], "%s"); // Tentatively' % (g.split(".")[-1], f)
				pspDict.pop(g)
				found=True
				break
	if not found:
		print "// " + f + " not found (" + str(len(psx)) + " bytes)"

	