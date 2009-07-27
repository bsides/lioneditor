import shapesmes as shapes
from PIL import Image

data = open("spr/WEP.SPR","rb").read()
print(len(data))
part1=shapes.buildPixels(data[0x200:0x8200])
part2=shapes.buildPixels(data[0x8400:0x10400])
part3=shapes.buildPixels(data[0x10600:])

size=(256,256)

i=0
size = (256, len(part1)/256);
print size
im = Image.new("RGBA", size, (0,0,0,0))
shapes.drawPixelsWithPaletteOnImage(part1, shapes.buildPalette(data[i*32:i*32+32]), im)
im.save("part1.palette%02d.png"%(i), "PNG")

size = (256, len(part2)/256);
print size
im = Image.new("RGBA", size, (0,0,0,0))
shapes.drawPixelsWithPaletteOnImage(part2, shapes.buildPalette(data[0x8200+i*32:0x8220+i*32]), im)
im.save("part2.palette%02d.png"%(i), "PNG")

size = (256, len(part3)/256);
print size
im = Image.new("RGBA", size, (0,0,0,0))
shapes.drawPixelsWithPaletteOnImage(part3, shapes.buildPalette(data[0x10400+i*32:0x10420+i*32]), im)
im.save("part3.palette%02d.png"%(i), "PNG")

