bytes = [] # 108
dates = [] # 96

shift = 0
start = 162


for i in range(start,start + 96):
#for i in [start]*96:
  bits = (i & 0x1FF)
  newbits = [(bits << shift) & 0xFF, ((bits << shift) & 0xFF00) >> 8]
  dates.append(newbits)
  print i, newbits
  shift += 1
  if (shift == 8): shift = 0

#AAAAAAAA 
#BBBBBBBA 
#CCCCCCBB 
#DDDDDCCC 
#EEEEDDDD 
#FFFEEEEE 
#GGFFFFFF 
#HGGGGGGG 
#HHHHHHHH

for j in range(0,96,8):
	bytes.append(dates[j+0][0])
	for i in range(7):
	  bytes.append(dates[j+i][1] | dates[j+i+1][0])
	bytes.append(dates[j+7][1])

for b in bytes:
  print "%02X" % (b),
print len(bytes)