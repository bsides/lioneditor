# coding: latin-1

O#PEN.LZW NOT used
#
#
#WORLD.LZW is true file for random character names
import sys
d={
0x00:"0",0x01:"1",0x02:"2",0x03:"3",0x04:"4",0x05:"5",0x06:"6",0x07:"7",
0x08:"8",0x09:"9",0x0a:"A",0x0b:"B",0x0c:"C",0x0d:"D",0x0e:"E",0x0f:"F",
0x10:"G",0x11:"H",0x12:"I",0x13:"J",0x14:"K",0x15:"L",0x16:"M",0x17:"N",
0x18:"O",0x19:"P",0x1a:"Q",0x1b:"R",0x1c:"S",0x1d:"T",0x1e:"U",0x1f:"V",
0x20:"W",0x21:"X",0x22:"Y",0x23:"Z",0x24:"a",0x25:"b",0x26:"c",0x27:"d",
0x28:"e",0x29:"f",0x2a:"g",0x2b:"h",0x2c:"i",0x2d:"j",0x2e:"k",0x2f:"l",
0x30:"m",0x31:"n",0x32:"o",0x33:"p",0x34:"q",0x35:"r",0x36:"s",0x37:"t",
0x38:"u",0x39:"v",0x3a:"w",0x3b:"x",0x3c:"y",0x3d:"z",0x3e:"!",0x40:"?",
0x42:"+",0x44:"/",0x46:":",0x5f:".",0x8d:"(",0x8e:")",0x91:'"',
0x93:"'",0x95:" ",
0xe0:"<Ramza>",
0xF8:"\n",
0xfa:" ",0xfb:"[lista]",0xfc:"[finelista]",
0xFE:"{END}\n\n",
0xff:"[Close]",
0xE200:"[Delay 00]",0xE201:"[Delay 01]",
0xE202:"[Delay 02]",0xE203:"[Delay 03]",0xE205:"[Delay 05]",0xE206:"[Delay 06]",
0xE20A:"[Delay 0A]",0xE20F:"[Delay 0F]",0xE214:"[Delay 14]",0xE21E:"[Delay 1E]",
0xE23C:"[Delay 3C]",0xE250:"[Delay 50]",0xE25A:"[Delay 5A]",0xE278:"[Delay 78]",
0xE308:"[Portrait]",0xE300:"[Dialog]",
0xD11D:"-",0xD11F:"[X]",
0xD129:"*",
0xD9C7:"[Triangle]",
0xD9C8:"[Square]",0xD9CA:"[Circle]",0xD9B9:"[Symbol B9]",0xD9C5:"~",
0xDA03:"[Symbol 03]",0xDA0A:"[Symbol 0A]",
0xDA62:"é",0xDA63:"è",0xDA61:"à",0xDA65:"ú",
0xDA66:"ù",0xDA68:"-",0xDA71:"§",0xDA72:"=",
0xDA74:",",0xDA75:";"}

def charsToInts(chars):
  result = [0] * len(chars)
  for i in range(len(chars)): result[i] = ord(chars[i])
  return result

def bytesToUInt32(bytes):
  result = 0
  result |= bytes[0]
  result |= bytes[1] << 8
  result |= bytes[2] << 16
  result |= bytes[3] << 24
  return result
  
def intsToChars(ints):
  result = [0] * len(ints)
  for i in range(len(ints)): result[i] = chr(ints[i])
  return ''.join(result)

def translateString(string):
  result = []
  for i in range(len(string)):
    if d.has_key(ord(string[i])):
      result.append(d[ord(string[i])])
    else:
      result.append(string[i])
  return ''.join(result)
  
bytes = charsToInts(open("OPEN.LZW.psp").read())

offsets=[]
for i in range(32):
  offsets.append(bytesToUInt32(bytes[i*4:(i+1)*4]))

files=[]
bytes=bytes[0x80:]

for i in range(len(offsets)):
  if (i+1)<len(offsets):
    files.append(intsToChars(bytes[offsets[i]:offsets[i+1]]))
  else:
    files.append(intsToChars(bytes[offsets[i]:]))

stuff=files[int(sys.argv[1])].split("\xFE")
for i in range(len(stuff)):
  stuff[i] = translateString(stuff[i])
print stuff