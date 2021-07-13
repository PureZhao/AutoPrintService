# AutoPrintService
#### Automatically print Word and PDF in specialized name in LAN<br>


## Environment
- Visual Studio 2012
- .NetFramework 4.7.1
- 
## How to use
1. Modify the **fileFolder** in main.cs to the path of that the directory which is used to store documents(Microsoft Word or PDF)
2. Compile to a Windows Application
3. 
If helpful, pleased.<br>
1.由于开发时间短，目前自动打印只支持Word(因为Word文档占大多数)，Excel，图片，PDF仍需要老师们手动上传后自己去一体机上打印，后续有时间再补全其他的
2. 因为开发时间短，对于文件名的命名有一定要求，即规范，上传时请按
“份数,从某一页打印到某一页,自定义区域”格式进行命名(请留意不要改掉文件后缀名)
份数，即你要打印几份，比如此位置填3，就是打印3份 
从某一页打印到某一页，即一份文档挑其中连续的几页打印，比如填写3-4就是打印第三页和第四页，注意:填写0-0是打印整份文档
自定义区域，用于防止重名，可随意填写但不能为空，最好填写自己的名字或者名字拼音首字母缩写
举例:文件名命名为3,4-5,hfjsjndvf，那么该文件上传后打印机会自动打印三份该文档4-5页的内容
文件名命名为2,0-0,sjcbdkek，那么该文件上传后打印机会自动打印2份全部页的内容
3. 再次重申请按规范命名，不规范命名上传系统，文件会被立即删除！(Excel 图片 PDF暂时不会被删除)
4.自动打印服务每5秒扫描一次，有文档才进行打印，打印默认为双面打印
5.自动打印服务每10分钟删除已打印文档，每次重启自动打印服务存放文档的文件夹会被清空(包括Excel 图片 PDF)
6.自动打印服务配备有打印日志，每天可以看到打印了什么，谁打印的，几点打印的等信息
7.请着重注意第2条
