#import copy

psxcharmap={
0x40:u"?",
0xD040:u"?",
0xD9C9:u"?",
0xB2:u"\u266A",
0xD0B2:u"\u266A",
0xD0E7:u"\u2014",
0xD117:u"\u2014",
0xD0E8:u"\u300C",
0xD118:u"\u300C",
0xD0EB:u"\u22EF",
0xD11B:u"\u22EF",
0xD0EF:u"\xD7",
0xD11F:u"\xD7",
0xD120:u"\xF7",
0xD121:u"\u2229",
0xD122:u"\u222A",
0xD123:u"=",
0xDA70:u"=",
0xD0F4:u"\u2260",
0xD124:u"\u2260",
0xD9B5:u"\u221E",
0xD9B7:u"&",
0xD9B8:u"%",
0xD9B9:u"\u25CB",
0xD9BA:u"\u2190",
0xD9BB:u"\u2192",
0xD9C2:u"\u300E",
0xD9C3:u"\u300F",
0xD9C4:u"\u300D",
0xD9C5:u"\uFF5E",
0xD9C7:u"\u25B3",
0xD9C8:u"\u25A1",
0xD9CA:u"\u2665",
0xD9CB:u"\u2160",
0xD9CC:u"\u2161",
0xD9CD:u"\u2162",
0xD9CE:u"\u2163",
0xD9CF:u"\u2164",
0xDA00:u"\u2648",
0xDA01:u"\u2649",
0xDA02:u"\u264A",
0xDA03:u"\u264B",
0xDA04:u"\u264C",
0xDA05:u"\u264D",
0xDA06:u"\u264E",
0xDA07:u"\u264F",
0xDA08:u"\u2650",
0xDA09:u"\u2651",
0xDA0A:u"\u2652",
0xDA0B:u"\u2653",
0xDA0C:u"{Serpentarius}",
0xDA71:u"$",
0xDA72:u"\xA5",
0xDA74:u",",
0xDA75:u";",

0xD0ED:u"-",
0xD11D:u"-",

0x42:u"+",
0xD042:u"+",
0xD0EE:u"+",
0xD11E:u"+",

0x46:u":",
0xD046:u":",
0xD9BD:u":",

0x8D:u"(",
0xD08D:u"(",
0xD9BE:u"(",

0x8E:u")",
0xD08E:u")",
0xD9BF:u")",

0x91:u'"',
0xD091:u'"',
0xD9C0:u'"',
0xDA77:u'"',

0x93:u"'",
0xD093:u"'",
0xD9C1:u"'",
0xDA76:u"'",

0x8B:u"\xB7",
0xD08B:u"\xB7",
0xD9BC:u"\xB7",

0x44:u"/",
0xD044:u"/",
0xD9C6:u"/",

0xD0F5:u">",
0xD125:u">",

0xD0F6:u"<",
0xD126:u"<",

0xD0F7:u"\u2267",
0xD127:u"\u2267",

0xD128:u"\u2266",

0xFA:u" ",
0xD0FA:u" ",
0xD12A:u" ",
0xDA73:u" ",

0x5F:u".",
0xD05F:u".",
0xD0E9:u".",
0xD119:u".",
0xD0EC:u".",
0xD11C:u".",
0xD9B6:u".",

0x3E:u"!",
0xD03D:u"!",
0xD0EA:u"!",
0xD11A:u"!",

0xB5:u"*",
0xD0B5:u"*",
0xD0E1:u"*",
0xD111:u"*",
0xD0F9:u"*",
0xD129:u"*",
0xD0FB:u"*",
0xD12B:u"*",
0xD0FC:u"*",
0xD12C:u"*",
0xD0FD:u"*",
0xD12D:u"*",
0xD0FE:u"*",
0xD12E:u"*",
0xD0FF:u"*",
0xD12F:u"*",
0xD130:u"*",
0xD131:u"*",
0xD132:u"*",
0xE0:u"{Ramza}",
0xF8:u"\\n",
0xFB:u"{Begin List}",0xFC:u"{End List}",
0xFE:u"{END}",
0xFF:u"{Close}"
}

for i in range(10):
  psxcharmap[i] = str(i)
  psxcharmap[0xd000+i]=str(i)
for i in range(ord("A"),ord("Z")+1):
  psxcharmap[i-ord("A")+0x0A]=chr(i)
  psxcharmap[i-ord("A")+0x0A + 0xD000]=chr(i)
for i in range(ord("a"),ord("z")+1):
  psxcharmap[i-ord("a")+0x24]=chr(i)
  psxcharmap[i-ord("a")+0x24 + 0xD000]=chr(i)
for i in range(256):
  psxcharmap[0xE200+i] = "{Delay %02X}" % (i)
  psxcharmap[0xE300+i] = "{Color %02X}" % (i)

pspcharmap={}
for i in psxcharmap.keys(): pspcharmap[i]=psxcharmap[i]
#pspcharmap=copy.deepcopy(psxcharmap)
pspcharmap[0x95] = u" "
pspcharmap[0xDA60] = u"\xE1"
pspcharmap[0xDA61] = u"\xE0"
pspcharmap[0xDA62] = u"\xE9"
pspcharmap[0xDA63] = u"\xE8"
pspcharmap[0xDA64] = u"\xed"
pspcharmap[0xDA65] = u"\xFA"
pspcharmap[0xDA66] = u"\xF9"

charmap = pspcharmap

def getNextChar(s, pos):
  resultPos = pos+1
  val=s[pos]
  key=val
  if (val >= 0xD0 and val <= 0xDA) or val == 0xE2 or val==0xE3:
    nextVal = s[pos+1]
    resultPos+=1
    key=val*256+nextVal
  
  if charmap.has_key(key):
    result = charmap[key]
  else:
    result = "{0x%02X}" % (key)
  
  return (result, resultPos)
  
def stringToBytes(s):
  result=[0]*len(s)
  for i in range(len(s)):
    result[i]=ord(s[i])
  return result
  
def bytesToString(bytes):
  result = [0]*len(bytes)
  for i in range(len(bytes)):
    result[i]=chr(bytes[i])
  return "".join(result)
  