# tarbr

## Description
Tool to combine files/folder into a single tarball file and compressed it using Brotli/GZip/ZLib/BZip2 algorithm. Support of Zip/7Zip compression that combine files/folders into one file and uses Zip/7Zip compression without tarball. The assembly name of this project is SplitJoin.

Has feature to split the output file into multiple files using size or count and support chaining.

```
Please specify switch :
   -TB <filename> <directory> ....           Tarball list of <filename> <directory> and Brotli Compress
   -UNTB <filename> <directory> ....         Decompress Brotli tarball and Extract it
   -TG <filename> <directory> ....           Tarball list of <filename> <directory> and GZip Compress
   -UNTG <filename> <directory> ....         Decompress GZip tarball and Extract it
   -TBZ <filename> <directory> ....          Tarball list of <filename> <directory> and BZip2 Compress
   -UNTBZ <filename> <directory> ....        Decompress BZip2 tarball and Extract it
   -TZ <filename> <directory> ....           Tarball list of <filename> <directory> and ZLib Compress
   -UNTZ <filename> <directory> ....         Decompress ZLib tarball and Extract it
   -SPLITCOUNT <filename> <count>            Splitting <filename> by count with equal size
   -SPLITSIZE <filename> <eg:1KB, 1MB, 1GB>  Splitting <filename> by size
   -JOIN <filename>                          Joining <filename>.1 <filename>.2 etc into <filename>
   -CRC <filename>                           CRC checksum of a <filename>
   -BR <filename>                            Brotli compress <filename>
   -UNBR <filename>                          Brotli decompress <filename>
   -GZ <filename>                            GZip compress <filename>
   -UNGZ <filename>                          GZip decompress <filename>
   -BZ2 <filename>                           BZip2 compress <filename>
   -UNBZ2 <filename>                         BZip2 decompress <filename>
   -ZLIB <filename>                          ZLib compress <filename>
   -UNZLIB <filename>                        ZLib decompress <filename>
   -TAR <filename> <directory> ....          Tarball list of <filename> <directory> into a single file
   -UNTAR <directory>                        Extract tarball into a directory
   -ZIP <filename> <directory> ....          Zip list of <filename> and <directory> into a single file
   -UNZIP <directory>                        Extract zip into a directory
   -7Z <filename> <directory> ....           7Zip list of <filename> and <directory> into a single file
   -UN7Z <directory>                         Extract 7Zip into a directory
Extra options per switch
   -O <output>                               Specifying the output file/directory name for the switch
   -F                                        Force overwrite existing files for the switch
   -P <password>                             Providing password to compress/decompress for Zip and 7Zip
   -SF <file1> <file2> <file3>               For -UNTAR -UNZIP and -UN7Z to extract specific file
General info
   -I                                        Program information

Chaining : Tar folders and files to a single file (-TAR and -F force overwrite) output to filename (-O)
Brotli compress (-BR) the tar file (if without specifying output filename default to <filename>.br)
(-F force overwrite) and split it into 1MB per file, resulting in final files : '<filename>.1',
'<filename>.2', etc. Chaining may generate temporary file that is not needed, eg below result in
'output.tar', 'output.tar.br' are temp files. Output files that are required is :
'output.tar.br.1', 'output.tar.br.2' etc

   -TAR <folder1> <folder2> <file1> -F -O output.tar -BR -O output.tar.br -F -SPLITSIZE 1MB

Joining 'output.tar.br.1', 'output.tar.br.2' (etc) back into 'output.tar.br' and decompressed it
   to 'output.tar' and untar it

   -JOIN output.tar.br -F -UNBR -O output.tar -F -UNTAR

TAR folders and files and compress to brotli file (-BR), for GZip -GZ, BZip2 -BZ2, ZLib -ZLIB

   -TAR <folder1> <folder2> <file1> <file2> -F -O output.tar -BR -F -O output.tar.br

or shortform to tarball and compress -TB (brotli) (for Gzip use -TG, for BZip2 use -TBZ
for ZLib use -TZ) :

   -TB <folder1> <folder2> <file1> <file2> -F -O output.tar.br

Decompress brotli file (-UNBR) and extract the tar, for GZip -UNGZ, BZip2 -UNBZ2, ZLib -UNZLIB

   -UNBR output.tar.br -O output.tar -F -UNTAR

or shortform to decompress and extract -UNTB (brotli) (for GZip use -UNTG, for BZip2 use -UNTBZ
for ZLib use -UNTZ) :

   -UNTB output.tar.br -O output

Compression and decompression for Brotli using -BR and -UNBR. For GZip use -GZ and -UNGZ, for
BZip2 use -BZ and -UNBZ and for ZLib use -ZLIB and -UNZLIB
Chaining is also available for GZip/ZLib/BZip2 compression algorithm. For Zip format, it already
has packaging files together with compression, it is not available for tarball and only available
to be chained for -SPLITSIZE or -SPLITCOUNT
```

## Brotli Normal Usage
Merge multiple files/folders into a single tarball file and compress it using brotli compression

```
   -TB <filename> <directory> -O "output.tar.br" -F
```

Output is "output.tar.br" and has a temporary file name "output.tar" that can be safely deleted

Decompress a tarball brotli compressed file and extract the tarball into folder

```
   -UNTB "output.tar.br" -O "outputfolder" -F
```
Output is a folder name "outputfolder" that contains the decompressed and extracted files, a temporary file "output.tar" is created and can be safely deleted.

Brotli compression is 10 times slower than normal gzip operations but it yield a good compression ratio. During brotli compression the program needs longer time to complete comparing to other compression algorithm.

## ZIP Normal Usage
Merge multiple files/folders into a single Zip file with Zip compression

```
   -ZIP <filename> <directory> -O "output.zip" -F
```

Output is "output.zip"

Decompress a Zip compressed file and extract the Zip into folder

```
   -UNZIP "output.zip" -O "outputfolder" -F
```
Output is a folder name "outputfolder" that contains the decompressed and extracted files

## 7Zip Normal Usage
Merge multiple files/folders into a single 7Zip file with 7Zip compression

```
   -7Z <filename> <directory> -O "output.7z" -F
```

Output is "output.7z"

Decompress a 7z compressed file and extract the 7z into folder

```
   -UN7Z "output.7z" -O "outputfolder" -F
```
Output is a folder name "outputfolder" that contains the decompressed and extracted files

## GZip Normal Usage
Merge multiple files/folders into a single tarball file and compress it using GZip compression

```
   -TG <filename> <directory> -O "output.tar.gz" -F
```

Output is "output.tar.gz" and has a temporary file name "output.tar" that can be safely deleted

Decompress a tarball GZip compressed file and extract the tarball into folder

```
   -UNTG "output.tar.gz" -O "outputfolder" -F
```
Output is a folder name "outputfolder" that contains the decompressed and extracted files, a temporary file "output.tar" is created and can be safely deleted

## BZip2 Normal Usage
Merge multiple files/folders into a single tarball file and compress it using BZip2 compression

```
   -TBZ <filename> <directory> -O "output.tar.bz2" -F
```

Output is "output.tar.bz2" and has a temporary file name "output.tar" that can be safely deleted

Decompress a tarball BZip2 compressed file and extract the tarball into folder

```
   -UNTBZ "output.tar.bz2" -O "outputfolder" -F
```
Output is a folder name "outputfolder" that contains the decompressed and extracted files, a temporary file "output.tar" is created and can be safely deleted


## ZLib Normal Usage
Merge multiple files/folders into a single tarball file and compress it using ZLib compression

```
   -TZ <filename> <directory> -O "output.tar.zlib" -F
```

Output is "output.tar.zlib" and has a temporary file name "output.tar" that can be safely deleted

Decompress a tarball ZLib compressed file and extract the tarball into folder

```
   -UNTZ "output.tar.zlib" -O "outputfolder" -F
```
Output is a folder name "outputfolder" that contains the decompressed and extracted files, a temporary file "output.tar" is created and can be safely deleted

## Chaining
There are several compressions methods, GZip using switch -GZ (-UNGZ to decompress), Brotli using switch -BR (-UNBR to decompress), BZip using switch -BZ2 (-UNBZ2 to decompress) and ZLib using switch -ZLIB (-UNZLIB to decompress). Compression modes can be made per single file, but normally it is chained or called after -TAR (merging of files/folders into one file) and then compress it resulting in .tar.gz (GZip) or .tar.br (Brotli). Zip/7Zip compression can compress directly into a single file using -ZIP switch (-UNZIP to decompress) (-7Z for 7Zip to compress and -UN7Z to decompress).

During chaining such as -BR switch, it does not need an input filename, because the input filename is the output of previous switch, however it can have extra options such as -F to force overwrite when the file already exist and -O to specify an output filename without using the default name. Each of the switch can has its own extra options (-F or -O).

After a compression is done, the command can chain for example into -SPLITSIZE 1MB, in order to split the output into multiple files ending with <filename>.1 <filename>.2 for distributions and join it back using -JOIN <filename>.
   
## Download
At right side, click Releases and from the assets download the file for your operating system. For Windows x64 machine the smallest binary available is tarbr-win-x64-aot.exe. Find the file that matches your Operating System and Architecture, while -vs means it is compiled as Intermediate Language, -bflat means it is compiled to native using bflat and -aot means it is compiled to native using NativeAOT.

Download the executable file and rename it to tarbr.exe and you use it via command prompt and CD into the directory or add it into PATH Environment Variable.
```
tarbr.exe -TB <files> <folders> -F -O output.tar.br
tarbr.exe -UNTB output.tar.br -F -O output
```
If files or folders contains space, it must be enclosed with quote for example 
```
tarbr.exe -TB "c:\my data\file.txt" -F -O output.tar.br
tarbr.exe -UNTB output.tar.br -F -O "my output"
```

## Example
Brotli compress "C:\Data\my file1.docx", "C:\Data\my file2.ppt" files and "C:\Data\Sheets" folder
```
tarbr.exe -TB "C:\Data\my file1.docx" "C:\Data\my file2.ppt" "C:\Data\Sheets" -F -O output.tar.br
```
Output is output.tar.br which contains the two files and one folder.

Copy the output to somewhere and decompress it
```
tarbr.exe -UNTB output.tar.br -F -O output
```
Output is "output" folder that contains the two files and a folder name Sheets
   
Notice we uses -TB and -UNTB for Brotli algorithm to compress/decompress. To use ligher/faster such as GZip algorithm change -TB to -TG and -UNTB to -UNTG. You can change to other algorithm such as BZip2 by which uses -TBZ and -UNTBZ, or ZLib algorithm which uses -TZ and -UNTZ respectively.
   
Example of GZip algorithm
   
```
tarbr.exe -TG "C:\Data\my file1.docx" "C:\Data\my file2.ppt" "C:\Data\Sheets" -F -O output.tar.gz
```
To decompress

```
tarbr.exe -UNTG output.tar.gz -F -O output
```
The extension .gz is because it is GZip compress, .br is because it is Brotli compress, for BZip2 uses extension .bz2 and for Zlib uses extension .zlib
