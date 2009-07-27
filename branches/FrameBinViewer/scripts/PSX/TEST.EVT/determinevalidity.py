# coding: latin-1

d={
0x00:"0",0x01:"1",0x02:"2",0x03:"3",0x04:"4",0x05:"5",0x06:"6",0x07:"7",
0x08:"8",0x09:"9",0x0a:"A",0x0b:"B",0x0c:"C",0x0d:"D",0x0e:"E",0x0f:"F",
0x10:"G",0x11:"H",0x12:"I",0x13:"J",0x14:"K",0x15:"L",0x16:"M",0x17:"N",
0x18:"O",0x19:"P",0x1a:"Q",0x1b:"R",0x1c:"S",0x1d:"T",0x1e:"U",0x1f:"V",
0x20:"W",0x21:"X",0x22:"Y",0x23:"Z",0x24:"a",0x25:"b",0x26:"c",0x27:"d",
0x28:"e",0x29:"f",0x2a:"g",0x2b:"h",0x2c:"i",0x2d:"j",0x2e:"k",0x2f:"l",
0x30:"m",0x31:"n",0x32:"o",0x33:"p",0x34:"q",0x35:"r",0x36:"s",0x37:"t",
0x38:"u",0x39:"v",0x3a:"w",0x3b:"x",0x3c:"y",0x3d:"z",
0x42:"+",0x44:"/",0x46:":",0x8d:"(",0x8e:")",0x91:'"',
0xe0:"[RAMZA]",
0xF8:"\n",
0xfb:"[BeginOptions]",0xfc:"[EndOptions]",
0xFE:"{END}",
0xff:"[Close]",
0xE200:"[Delay 00]",0xE201:"[Delay 01]",
0xE202:"[Delay 02]",0xE203:"[Delay 03]",0xE205:"[Delay 05]",0xE206:"[Delay 06]",
0xE20A:"[Delay 0A]",0xE20F:"[Delay 0F]",0xE214:"[Delay 14]",0xE21E:"[Delay 1E]",
0xE23C:"[Delay 3C]",0xE250:"[Delay 50]",0xE25A:"[Delay 5A]",0xE278:"[Delay 78]",
0xE308:"[Portrait]",0xE300:"[Dialog]",
0xD11F:"[Cross]",
0xD129:"*",
0xD9C7:"[Triangle]",
0xD9C8:"[Square]",0xD9CA:"[Circle]",0xD9B9:"[Symbol B9]",0xD9C5:"~",
0xDA03:"[Symbol 03]",0xDA0A:"[Symbol 0A]",
0xDA62:"é",0xDA63:"è",0xDA61:"à",0xDA65:"ú",
0xDA66:"ù",0xDA71:"§",0xDA72:"=",
0xDA75:";",
0xDA74:",",

#duplicate symbols
0xD11D:"-",0xDA68:"-",
0xD9B6:".",0x5f:".",
0xfa:" ",0x95:" ",
0x93:"'",0xD9C1:"'",
0x40:"?",0xD9C9:"?",
0x3e:"!",0xD11A:"!"
}

notFound={}

def toUShort(b1, b2):
  return b2 * 256 + b1

def getBytes(filename):
  f = open(filename,"rb")
  b = f.read()
  f.close()
  result = [0]*len(b)
  for i in range(len(b)):
    result[i] = ord(b[i])
  return result

def findNextE300(f):
  pos = 0
  while pos+1 < len(f):
    if f[pos]==0xe3 and f[pos+1]==0x00: return pos
    pos += 1
  return -1

def numberToFFTBytes(num):
  s = str(num)
  result = [0]*len(s)
  for i in range(len(s)):
    result[i] = int(s[i])
  return result

def getString(bytes):
  result = [0]*len(bytes)
  for i in range(len(bytes)):
    result[i] = chr(bytes[i])
  return ''.join(result)
  
def processFile(f):
  result = []
  position = toUShort(f[0],f[1])
  while position < len(f):
    b = f[position]
    position += 1
    if d.has_key(b):
      result.append(d[b])
    elif (b == 0xda) or (b == 0xD1) or (b == 0xe2) or (b == 0xe3) or (b == 0xd9):
      c = f[position]
      position += 1
      h = toUShort(f[position-1],f[position-2])
      if d.has_key(h):
        result.append(d[h])
      else:
        result.append("<0x%04X>" % (h))
        incrementKey("<0x%04X>" % (h))
    else:
      result.append("<0x%02X>" % (b))
      incrementKey("<0x%02X>" % (b))
  result2 = ''.join(result).split("{END}")
  for i in range(len(result2)):
    result2[i] = "<entry>%s</entry>\n" % (result2[i])
  return ''.join(result2)
  
  
bytes = getBytes("TEST.EVT")


known=[4, 8, 16, 20, 28, 32, 50, 54, 58, 62, 68, 76, 86, 90, 104, 108, 116, 120, 138, 180, 234, 276, 294, 298, 308, 316, 320, 340, 344, 348, 380, 402, 406, 422, 430, 448, 462, 466, 498, 510, 520, 540, 550, 566, 580, 592, 596, 600, 618, 654, 658, 662, 690, 704, 716, 732, 748, 752, 768, 778, 854, 858, 864, 884, 914, 978]
state=["unknown"]*1000
files={}
for i in range(len(known)):
  state[known[i]] = "good"
  
def findMatch(s):
  for i in range(len(known)):
    if s[:100] == files[known[i]][:100]: return known[i]
  return -1
  
# go through file, get all 1000 entries
# get all known entries
# go back through entries and determine which match

position = 0
num = 0
while position < 0x3E8000:
  if num == 800 or num == 802 or num == 804 or num == 808:
    f=bytes[position:position+0x1800+0x800]
    position += 0x1800 + 0x800
    num += 1
  elif num % 2 == 0:
    f=bytes[position:position+0x1800]
    position += 0x1800
  else:
    f=bytes[position:position+0x800]
    position += 0x800
  
  if f[0] == '\xF2' and f[1] == '\xF2' and f[2] == '\xF2' and f[3] == '\xF2':
    filename = str(num) + ".dummy"
  elif (toUShort(f[0], f[1]) > 0x1800 and len(f) == 0x1800) or (toUShort(f[0], f[1]) > 0x0800 and len(f) == 0x800):
    filename = str(num) + ".partial"
  else:
    pos = toUShort(f[0],f[1])
    filename = str(num)
    print num
    files[num]=f[pos:]
  num +=1
    
for i in files.keys():
  if state[i] == "unknown":
    match = findMatch(files[i])
    if match != -1:
      state[i] = "dupe%d" % (match)

for i in range(len(state)):
  print i, state[i]