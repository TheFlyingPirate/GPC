.start
	LDA .d1
	LDB .d1+1
	ADD .d2
	JMP .hello
.d1
	DAT 8 0x03
.d2
	DAT 8 0x02
'Hello World'
'TEST'
<include>E:\Development\GPC Emulator\Emulator\GPCEmulatorDotNET\GPCASM\bin\Debug\hello.asm</include>