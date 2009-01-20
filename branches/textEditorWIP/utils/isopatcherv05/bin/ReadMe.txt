
                 iso patcher v0.5
                 =================

  iso patcher 是一个把数据写入iso的程序。
  支持iso9660各种格式，PS, SS机的iso.
  支持自动计算光盘校验。

  本项目开源，遵循 GNU GPL 规范，不得用于任何商业用途等。
  源代码可以在vc6以及VC.NET编译。如果你有改进可以提交给我们。


*************************************************************
 历史版本 History of changes:

 2004/12/27 v0.5 - Agemo
 - 增加把原始数据写到指定位置的功能

 2004/04/18 v0.4 - Agemo
 - code cleanup
 - 增加 mode1 支持，mode2 form2 支持（未测试）
 - ecc edc 计算算法不用汇编版本，改用C语言版
 - 加入win32 dll版的支持，为了其他语言直接调用
 - 注释的行标记加入半角#

 2004/01/15 v0.3 - xade
 - LST 格式修整，每项改为一行
 - LST 格式加入直接写入字节的支持，而不是必须外部的文件。
 - LST 忽略空行

 2002/11/06 v0.2 - xade
 - 加强了对 LST 文件的错误检测，修正了上一版在检查 LST
   文件时的一些小问题，虽然它们并不影响使用^^
 - 加入了注释的支持，以半角分号;开头的行为注释行。
 - 增加自动重算 EDC/ECC 的功能，现在无须再 patch 后使用
   ECCRegen 之类的软件进行重算了（Yeah~~~~）^^
   可以用开关 /e 关闭这个功能:)

 2002/10/16 v0.1 - xade
 - 由于汉化《月下夜想曲》的需要，我自己写了这个小咚咚，
   提供最基本的 patch 功能，但是只能用于 playstation
   光盘的 mode 2 form 1 数据，不能用它修改 XA 格式的数据。

*************************************************************
  使用方法：

  exe 版需要在window的命令行提示符下运行:
  isopatch list_file iso_file [/mode] [/e]

  /mode : iso mode, optional params.
          /M1   = mode1
          /M2F1 = mode2 form1 (default) (playstation)
          /M2F2 = mode2 form2
  /e: calculating ecc and edc OFF. optional params. (default ON)

  all params are case insensitive.

*************************************************************

无论那种格式，每个扇区2352字节，实际数据是2048字节，其余是校验等。

如下步骤：
01. 把你要修改的数据在虚拟光驱内复制出来。
    或者按直接从iso内复制出来，但是要复制的必须是数据区以内的部分。
    然后可以用十六进制编辑器修改。

02. 在十六进制编辑器中对 ISO 查找这段数据的起始地址，
    记下来，假设是 0x0005f04c

03. 新建一个文本文件，称之为patch list文件。
    （更详细的说明例子见 example\list.txt）

    加入起始地址和修改后的数据文件名，像上面的就是这样：
        0005f04c,holybell.bin
    然后保存这个文件，比如保存为 kill.lst

04. 如果还有其他部分，重复01-03的过程

05. 将上面涉及到的文件（holybell.bin 及其同类和 kill.lst）和主程序、
    目标镜像文件（假设为 dark01.bin ）放在同一个目录下，
    然后执行以下命令：
        isopatch kill.lst dark01.bin
    
    如果你有特殊的要求不需要重算 EDC/ECC 的话，执行以下命令：
        isopatch kill.lst dark01.bin /e

    如果是特殊需要，比如mode1 iso的，
        isopatch kill.lst dark01.bin /M1


