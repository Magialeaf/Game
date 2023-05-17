#include<stdio.h>
int main()
{
	int hp0=100,mp=100,hp1=200,i=1,n,ac=0,k;
	printf("战斗开始,你的对手是草史莱姆,防御值每1点可减免百分之10伤害，受到攻击清零。\n输入数字进行操作。\n");
	while(hp0>0&&hp1>0)
	{
		printf("————————————————\n");
		printf("当前自身hp为%d,mp为%d,ac为%d,对手hp为%d\n",hp0,mp,ac,hp1);
		printf("1 攻击\n");
		printf("2 技能—重击\n");
		printf("3 技能—回复\n");
		printf("4 防御\n");
		printf("请选择操作:");
		scanf("%d",&n);
		switch(n)
		{
		case 1:printf("扣除对方10hp\n");hp1-=10;break;
		case 2:if(mp>=35) {printf("消耗35mp扣除对方30hp\n");mp-=35;hp1-=30;} else {printf("mp不足!默认进行攻击\n");hp1-=10;}break;
		case 3:if(mp>=80) {printf("消耗80mp回复60hp且增加防御值5\n");mp-=80;hp0+=60;ac+=5;}  else {printf("mp不足!默认进行防御\n");ac+=2;}break;
		case 4:printf("防御值加2 \n");ac+=2;break;
		default:printf("输入错误!默认进行防御\n");ac+=2;
		}
		if(ac>10) ac=10;
		if(hp1<=0) i=-1;
		switch(i)
		{
		case 1:k=10*(10-ac)/10;ac=0;printf("草A了你一下 hp-%d\n",k);hp0-=k;i++;break;
		case 2:printf("草使用了草之力，下次攻击造成高额伤害\n");i++;break;
		case 3:k=50*(10-ac)/10;ac=0;printf("草A了你一下 hp-%d\n",k);hp0-=k;i++;break;
		case 4:printf("草使用了生草 自身hp+40\n");hp1+=40;i=1;break;
		}	
		printf("回合结束 mp回复20\n");
		mp+=20;
	}
	printf("————————————————\n");
	if(hp0>=0&&hp1<=0) printf("恭喜你获得胜利!\n");
	if(hp0<=0&&hp1>=0) printf("真可惜，下次一定获胜!\n");
	return 0;
}