#include<stdio.h>
#include<conio.h>
	int money;
	int lv,boss=1;
	int hp,hp1,hpu;mp,mpu;ac=0,ac1=0,ad,ad1,k,k1,k2;
	int cao=0,huo=0,lei=0,feng=0,yan=0;
FILE *fp;
void lvv()//人物等级判断
{
	if(lv==1) {hp=100;mp=100;mpu=20;ad=10;}
	if(lv==2) {hp=110;mp=100;mpu=20;ad=12;}
	if(lv==3) {hp=150;mp=150;mpu=25;ad=14;}
	if(lv==4) {hp=170;mp=150;mpu=25;ad=16;}
	if(lv==5) {hp=200;mp=200;mpu=25;ad=18;}
	if(lv==6) {hp=200;mp=200;mpu=30;hpu=10;ad=20;}
}

int people()//人物
{
	char n;
	if(hp<=0) return 1;
		printf("————————————————\n");
		printf("当前自身hp为%d,mp为%d,ac为%d,对手hp为%d,ac为%d\n",hp,mp,ac,hp1,ac1);
		printf("j 攻击\n");
		if(lv>=2) printf("u 技能—重击\n");
		if(lv>=2) printf("i 技能—回复\n");
		if(lv>=5) printf("o 技能—爆裂魔法\n");
		if(lv>=6) printf("l 技能—矢量转换\n");
		printf("k 防御\n");
		if(boss!=0) printf("y 逃跑\n");
		printf("请选择操作:");
		n=getch();
		system("cls");
		switch(n)
		{
		case 'j':k=ad*(10-ac1)/10;ac1=0;printf("扣除对方%dhp\n",k);hp1-=k;break;
		case 'u':if(lv>=2&&lv<4&&mp<35) {printf("操作失败!默认进行攻击\n");k=ad*(10-ac1)/10;hp1-=k;} if(lv>=4&&mp<40) {printf("操作失败!默认进行攻击\n");k=ad*(10-ac1)/10;hp1-=k;} if(lv==1) {printf("操作失败!默认进行攻击\n");k=ad*(10-ac1)/10;hp1-=k;} if(mp>=35&&lv>=2&&lv<4) {k=30*(10-ac1)/10;ac1=0;printf("消耗35mp扣除对方%dhp\n",k);mp-=35;hp1-=k;} if(mp>=35&&lv>=4) {k=80*(10-ac1)/10;ac1=0;printf("消耗40mp扣除对方%dhp\n",k);mp-=40;hp1-=k;} break;
		case 'i':if(lv>=2&&lv<4&&mp<70) {printf("操作失败!默认进行防御\n");ac+=2;} if(lv>=4&&mp<80) {printf("操作失败!默认进行防御\n");ac+=2;} if(lv==1) {printf("操作失败!默认进行防御\n");ac+=2;} if(mp>=80&&lv>=2&&lv<4) {printf("消耗70mp回复60hp且增加防御值4\n");mp-=70;hp+=60;ac+=4;} if(mp>=80&&lv>=4) {printf("消耗80mp回复90hp且增加防御值6\n");mp-=80;hp+=100;ac+=6;}  break;
		case 'o':if(mp>=180&&lv>=5) {k=400*(10-ac1)/10;ac1=0;k1=30*(10-ac)/10;ac1=0;printf("消耗180mp扣对方%dhp但自身扣%dhp\n",k,k1);mp-=200;hp1-=k;hp-=k1;}  else {printf("操作失败!默认进行攻击\n");}break;
		case 'l':if(lv>=6) {k=20;printf("将对方20hp转换为自身hp(无视对方护甲),但自身ac-10\n");hp+=20;hp1-=20;ac-=10;}  else {printf("操作失败!默认进行攻击\n");k=ad*(10-ac1)/10;hp1-=k;}break;
		case 'k':printf("防御值加2\n");ac+=2;break;
		case 'y':if(boss==0) printf("输入错误!默认进行防御\n");else return 1;break;
		default:printf("输入错误!默认进行防御\n");ac+=2;
		}
		if(ac>10) ac=10;
		return 0;
}

int c1()//草
{
	int z=0,b=0,i=1;
	printf("战斗开始,你的对手是草史莱姆,它会进行草系操作,输入字母进行操作.\n");
	lvv();
	if(lv==1) {hp1=20;ad1=5;ac1=0;}
	if(lv==2) {hp1=150;ad1=10;ac1=0;}
	if(lv==3) {hp1=220;ad1=12;ac1=0;}
	if(lv==4) {hp1=350;ad1=14;ac1=0;}
	if(lv>=5) {hp1=700;ad1=18;ac1=0;}
	while(z!=1&&b!=1)
	{
	if(lv==6&&ac<2) {ac=2;}
	z=people();
	if(z==1) break;
	if(hp1<=0) break;
	switch(i)
		{
		case 1:k1=ad1*(10-ac)/10;ac=0;printf("草史莱姆A了你一下 hp-%d\n",k1);hp-=k1;i++;break;
		case 2:printf("草史莱姆使用了草之力，下次攻击造成高额伤害\n");i++;break;
		case 3:k1=ad1*4*(10-ac)/10;ac=0;printf("草史莱姆A了你一下 hp-%d\n",k1);hp-=k1;i++;break;
		case 4:k1=lv*15;printf("草史莱姆使用了生草 自身hp+%d\n",k1);hp1+=k1;i=1;break;
		}	
		printf("回合结束 mp回复%d\n",mpu);
		mp+=mpu;
		if(lv==6) {printf("回合结束 hp回复20\n");hp+=20;}
	if(hp1<=0) b=1;
	}
	if(hp>0&&hp1<=0) {printf("恭喜你获得胜利!,金钱+%d\n",lv);money+=lv;if(lv>=5) {printf("草之心+1\n");cao++;}return 0;}
	if(hp<=0&&hp1>0) {printf("战斗失败!\n");return 1;}
	if(hp>0&&hp1>0) {printf("逃跑成功!\n");return 0;}
	return 0;
}

int c2()//火
{
	int z=0,b=0,i=1;
	printf("战斗开始,你的对手是火史莱姆,它会进行火系操作,输入字母进行操作.\n");
	lvv();
	if(lv==2) {hp1=250;ad1=15;ac1=0;}
	if(lv==3) {hp1=450;ad1=17;ac1=0;}
	if(lv==4) {hp1=740;ad1=22;ac1=0;}
	if(lv>=5) {hp1=1150;ad1=30;ac1=0;}
	while(z!=1&&b!=1)
	{
	if(lv==6&&ac<2) {ac=2;}
	z=people();
	if(z==1) break;
	if(hp1<=0) break;
	switch(i)
		{
		case 1:k1=ad1*(10-ac)/10;ac=0;printf("火史莱姆A了你一下 hp-%d\n",k1);hp-=k1;i++;break;
		case 2:k1=ad1*(10-ac)/10;ac=0;printf("火史莱姆使用了三连击Hp-%d -%d -%d\n",k1,ad1,ad1);hp-=k1;hp-=ad1;hp-=ad1;i++;break;
		case 3:k1=lv*10;printf("火史莱姆使用了火焰净魂 你的mp-%d\n",k1);mp-=k1;i++;break;
		case 4:printf("火史莱姆使用了火焰羽衣 自身ac+%d\n",lv);ac1+=lv;i=1;break;
		}	
		printf("回合结束 mp回复%d\n",mpu);
		mp+=mpu;
		if(lv==6) {printf("回合结束 hp回复20\n");hp+=20;}
	if(hp1<=0) b=1;
	}
	if(hp>0&&hp1<=0) {printf("恭喜你获得胜利!,金钱+%d\n",lv);money+=lv;if(lv>=5) {printf("火之心+1\n");huo++;}return 0;}
	if(hp<=0&&hp1>0) {printf("战斗失败!\n");return 1;}
	if(hp>0&&hp1>0) {printf("逃跑成功!\n");return 0;}
	return 0;
}

int c3()//雷
{
	int z=0,b=0,i=1;
	printf("战斗开始,你的对手是雷史莱姆,它会进行雷系操作,输入字母进行操作.\n");
	lvv();
	if(lv==2) {hp1=200;ad1=11;ac1=0;}
	if(lv==3) {hp1=270;ad1=14;ac1=0;}
	if(lv==4) {hp1=410;ad1=18;ac1=0;}
	if(lv>=5) {hp1=500;ad1=25;ac1=0;}
	while(z!=1&&b!=1)
	{
	if(lv==6&&ac<2) {ac=2;}
	z=people();
	if(z==1) break;
	if(hp1<=0) break;
	switch(i)
		{
		case 1:printf("雷史莱姆使用了麻痹 下次雷史莱姆继续攻击\n");
				k1=ad1*(10-ac)/10;ac=0;printf("雷史莱姆使用了重雷Hp-%d 你的ac-%d\n",k1,lv);hp-=k1;ac-=lv;i++;break;
		case 2:k1=ad1*3*(10-ac)/10;ac=0;printf("雷史莱姆使用了震雷Hp-%d\n",k1);hp-=k1;i++;break;
		case 3:k1=ad1*(10-ac)/10;ac=0;printf("雷史莱姆使用了净雷Hp-%d 你的mp-%d\n",k1,lv*5);hp-=k1;mp-=lv*5;i=1;break;
		}	
		printf("回合结束 mp回复%d\n",mpu);
		mp+=mpu;
		if(lv==6) {printf("回合结束 hp回复20\n");hp+=20;}
	if(hp1<=0) b=1;
	}
	if(hp>0&&hp1<=0) {printf("恭喜你获得胜利!,金钱+%d\n",lv);money+=lv;if(lv>=5) {printf("雷之心+1\n");lei++;}return 0;}
	if(hp<=0&&hp1>0) {printf("战斗失败!\n");return 1;}
	if(hp>0&&hp1>0) {printf("逃跑成功!\n");return 0;}
	return 0;
}

int c4()//风
{
	int z=0,b=0,i=1;
	printf("战斗开始,你的对手是风史莱姆,它会进行风系操作,输入字母进行操作.\n");
	lvv();
	if(lv==3) {hp1=350;ad1=40;ac1=0;}
	if(lv==4) {hp1=570;ad1=60;ac1=0;}
	if(lv>=5) {hp1=800;ad1=80;ac1=0;}
	while(z!=1&&b!=1)
	{
	if(lv==6&&ac<2) {ac=2;}
	z=people();
	if(z==1) break;
	if(hp1<=0) break;
	switch(i)
		{
		case 1:k1=ad1*(10-ac)/10;ac=0;printf("风史莱姆A了你一下 hp-%d\n",k1);hp-=k1;i++;break;
		case 2:printf("风史莱姆开启了风场，免疫下次物理攻击\n");ac1+=10;if(ac1>10) ac1=10;i++;break;
		case 3:;k1=mp;printf("风史莱姆使用了风散 清空你的蓝并在下回合结束时还回\n",k1);mp=0;i++;break;
		case 4:printf("风史莱姆使用了风镰 对你造成了真实伤害%d \n",lv*22);mp+=k1;hp-=lv*22;i=1;break;
		}	
		printf("回合结束 mp回复%d\n",mpu);
		mp+=mpu;
		if(lv==6) {printf("回合结束 hp回复20\n");hp+=20;}
	if(hp1<=0) b=1;
	}
	if(hp>0&&hp1<=0) {printf("恭喜你获得胜利!,金钱+%d\n",lv);money+=lv;if(lv>=5) {printf("风之心+1\n");feng++;}return 0;}
	if(hp<=0&&hp1>0) {printf("战斗失败!\n");return 1;}
	if(hp>0&&hp1>0) {printf("逃跑成功!\n");return 0;}
	return 0;
}

int c5()//岩
{
	int z=0,b=0,i=1;
	printf("战斗开始,你的对手是岩史莱姆,它会进行岩系操作，且ac永远大于%d点,输入字母进行操作.\n",lv);
	lvv();
	if(lv==4) {hp1=800;ad1=0;}
	if(lv>=5) {hp1=1500;ad1=0;}
	while(z!=1&&b!=1)
	{
	if(lv==6&&ac<2) {ac=2;}
	z=people();
	if(z==1) break;
	if(hp1<=0) break;
	if(ac1<6) ac1=lv;
	switch(i)
		{
		case 1:printf("岩史莱姆使用了岩盾 自身ac+4\n");ac1+=4;i++;break;
		case 2:k1=k*(lv-1);if(k1<0) k1=-k1;printf("岩史莱姆使用了刺甲 反弹上次自身受到的伤害并加%d倍 自身hp-%d\n",lv-1,k1);k=0;hp-=k1;i=1;break;
		}	
		printf("回合结束 mp回复%d\n",mpu);
		mp+=mpu;
		if(lv==6) {printf("回合结束 hp回复20\n");hp+=20;}
	if(hp1<=0) b=1;
	}
	if(hp>0&&hp1<=0) {printf("恭喜你获得胜利!,金钱+%d\n",lv);money+=lv;if(lv>=5) {printf("岩之心+1\n");yan++;}return 0;}
	if(hp<=0&&hp1>0) {printf("战斗失败!\n");return 1;}
	if(hp>0&&hp1>0) {printf("逃跑成功!\n");return 0;}
	return 0;
}

int C()//王
{
	int z=0,b=0,i=1,time=1;
	printf("战斗开始,你的对手是史莱姆王，它每回合可以使用两个史莱姆技能,输入字母进行操作.\n",lv);
	boss=0;
	lvv();
	hp1=2000;ad1=55;ac1=0;
	if(cao>0&&huo>0&&lei>0&&feng>0&&yan>0)
		{printf("史莱姆王受到史莱姆心的打击hp-800,攻击-10\n");hp1-=800;ad1-=10;}
	while(z!=1&&b!=1)
	{
	if(lv==6&&ac<2) {ac=2;}
	z=people();
	if(z==1) break;
	if(hp1<=0) break;
	switch(i)
		{
		case 1:printf("史莱姆王使用了岩火盾 自身ac+5且每回合扣除你8hp持续5回合\n");ac1+=5;k1=8*(10-ac)/10;ac=0;printf("hp-%d\n",k1);hp-=k1;i++;break;
		case 2:k2=mp;printf("史莱姆王使用了大风场 清空你的蓝并在下回合结束时还回且免疫下次物理伤害 \n");mp=0;ac1+=10;if(ac1>10) ac1=10;k1=8*(10-ac)/10;ac=0;printf("hp-%d\n",k1);hp-=k1;i++;break;
		case 3:k1=120*(10-ac)/10;ac=0;printf("史莱姆王使用了草雷破 造成两次巨额伤害 hp-%d %d\n",k1,ad1);hp-=k1;hp-=ad1;k1=8*(10-ac)/10;ac=0;printf("hp-%d\n",k1);hp-=k1;mp+=k2;i++;break;
    	case 4:printf("史莱姆王使用了元素转换 互换你的mp与hp并扣除你hp和mp30点(固定)\n");k1=hp;hp=mp;mp=k1;hp-=30;mp-=30;k1=8*(10-ac)/10;ac=0;printf("hp-%d\n",k1);hp-=k1;i++;break;
		case 5:printf("史莱姆王使用了终结 当你的生命小于%d时直接斩杀否则扣除%dac\n",80+time*10,5+time);if(hp<80+time*10) hp=-9999;else ac-=5+time;k1=8*(10-ac)/10;ac=0;printf("hp-%d\n",k1);hp-=k1;i=1;time++;break;
		}	
		printf("回合结束 mp回复%d\n",mpu);
		mp+=mpu;
		if(lv==6) {printf("回合结束 hp回复20\n");hp+=20;}
	if(hp1<=0) b=1;
	}
	if(hp>0&&hp1<=0) {printf("恭喜你获得胜利!,金钱+%d\n",lv);money+=lv;return 0;}
	if(hp<=0&&hp1>0) {printf("战斗失败!\n");return 1;}
	if(hp>0&&hp1>0) {printf("逃跑成功!\n");return 0;}
	return 0;
}

int xiang()//隐藏箱
{
	int n=1;
	if(lv==6) n--;
	if(lv==5&&n==1) {printf("lv+1 AD+2 MP恢复变成30 HP恢复变成20 AC常驻2 解锁史量转换 真视:地址8 7为假墙\n");lv++;}
	return 0;
}

int migong()//迷宫
{
	char a[10][10]={'0','0','0','0','0','0','0','0','0','0',
					'.','.','0','0','s','0','a','c','.','0',
					'0','.','0','.','.','0','0','0','.','0',
					'0','.','0','.','0','0','0','.','.','0',
					'0','.','c','.','.','c','.','.','0','0',
					'0','0','.','0','.','.','.','0','.','0',
					'0','.','.','.','0','.','0','.','.','0',
		     		'0','.','0','.','0','.','0','5','0','0',
					'0','c','.','.','.','c','0','.','C','.',
					'0','0','0','0','0','0','0','0','0','0'};
	int i=2,n=1,z=0,f=0;
	char input;
	char ad,ap,ac;
	ap=a[1][1];
	while(i!=9||n!=10)
	{
	if(f==1) break;
	printf("——————————————\n");
	printf("%c %c %c\n%c %d %c\n%c %c %c\n",a[i-2][n-2],a[i-2][n-1],a[i-2][n],a[i-1][n-2],z+1,a[i-1][n],a[i][n-2],a[i][n-1],a[i][n]);
	printf("你目前位于%d %d\n",i,n);
	printf("请输入移动方向(wsad)\n");
	printf("输入空格存档退出游戏\n");
	input=getch();
	system("cls");
	if(input=='w'){i--;if(i==4&&n==6) xiang();ad=a[i-1][n-1];if(ad==48) {printf("无效移动!\n");i++;}}
	if(input=='s'){i++;ad=a[i-1][n-1];if(ad==48) {printf("无效移动!\n");i--;}}
	if(input=='a'){n--;ad=a[i-1][n-1];if(ad==48) {printf("无效移动!\n");n++;}}
	if(input=='d'){n++;if(i==8&&n==7) n++;ad=a[i-1][n-1];if(ad==48) {printf("无效移动!\n");n--;}}
	if(input==' ')	return 0;
	if(i==2&&n==7)
	{
		printf("大门已打开!\n");
		a[1][6]=ap;a[7][7]=ap;
	}
	if(i==8&&n==8)
	{
		ac=a[7][7];
		if(ac==53)
		{
			printf("无效移动，大门未打开!\n");
			n--;
		}
	}
	if(i==2&&n==5) store();
	if(i==5&&n==3) f=c1();
	if(i==9&&n==2&&lv>=2) f=c2(); 
	if(i==9&&n==2&&lv<2) {printf("前面的史莱姆以后再来打吧\n");i--;}
	if(i==9&&n==6&&lv>=2) f=c3();
	if(i==9&&n==6&&lv<2) {printf("前面的史莱姆以后再来打吧\n");i--;}
	if(i==5&&n==6&&lv>=3) f=c4();
	if(i==5&&n==6&&lv<3) {printf("前面的史莱姆以后再来打吧\n");i++;}
	if(i==2&&n==8&&lv>=4) f=c5();
	if(i==2&&n==8&&lv<4) {printf("前面的史莱姆以后再来打吧\n");n++;}
	if(i==9&&n==9) f=C();
	}
	if(i==9&&n==10)
	printf("你目前位于9 10，恭喜通关!\n");
	else
	printf("游戏失败!\n");
	return 0;
}

int store()//商店
{
	int x;
	char n;
	while(n!='e')
	{
	switch(lv)
	{
	case 1:x=2;break;
	case 2:x=6;break;
	case 3:x=12;break;
	case 4:x=20;break;
	case 5:x=0;break;
	case 6:x=0;break;
	}
	printf("——————————————\n");
	printf("你当前等级为%d 有%d元 升级需要%d元\n q.确定升级 e.退出\n",lv,money,x);
	n=getch();
	system("cls");
	switch(n)
	{
	case 'q':if(lv==1&&money>=2) {printf("lv+1 AD+2 hp+10 解锁重击和恢复技能!\n");lv++;money-=2;break;}
			if(lv==2&&money>=6) {printf("lv+1 AD+2 hp+40 mp+50 mp恢复上升为25!\n");lv++;money-=5;break;}
			if(lv==3&&money>=12) {printf("lv+1 AD+2 hp+20  强化重击和恢复技能!\n");lv++;money-=15;break;}
			if(lv==4&&money>=20) {printf("lv+1 AD+2 hp+30 mp+50 解锁爆裂魔法!\n");lv++;money-=30;break;}
			if(lv==5) {printf("已达到5级 想再升级需要找到隐藏宝箱\n");break;}
			if(lv==6) {printf("你已满级\n");break;}
			printf("升级失败!金钱不够");break;
	case 'e':printf("退出成功!\n");break;
	default:printf("操作失败\n");
	}
	}
	return 0;
}

int  main()//主函数
{
	char n;
	printf("你好，欢迎来到史莱姆迷宫!输入空格开始游戏\n");
	if((fp=fopen("Record.txt","r"))==NULL)
		{
		printf("游戏存档丢失!已创造新存档!\n");
		fp=fopen("Record.txt","w");
		fprintf(fp,"1\t0\n");
		fclose(fp);
		}
	fp=fopen("Record.txt","r");
	fscanf(fp,"%d\t%d\n",&lv,&money);
	fclose(fp);
	n=getch();
	if(n==' ')
	{
	printf("1为人 0为墙 5为门 s为商店c为小怪C为BOSS .为路\n移动输入wsad 达到9 10即过关\n");
	printf("每一点防御可以减少10%的所受伤害 受到攻击清零\n");
	printf("当前lv为%d,money为%d\n",lv,money);
	migong();
	}
	fp=fopen("Record.txt","w");
	fprintf(fp,"%d\t%d\n",lv,money);
	fclose(fp);
	printf("成功退出游戏，金钱与等级保已保存!\n");
	printf("按任意键退出\n");
	n=getch();
	return 0;
}

