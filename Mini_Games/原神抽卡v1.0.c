#include<stdio.h>
#include<stdlib.h>
int p=0,x;

void card(i)
{
	if(i>=1&&i<=20) printf("恭喜你获得空!\t");
	if(i>=21&&i<=40) printf("恭喜你获得荧!\t");
	if(i>=41&&i<=60) printf("恭喜你获得迪卢克!\t");
	if(i>=61&&i<=80) printf("恭喜你获得可莉!\t");
	if(i>=810&&i<=100) printf("恭喜你获得胡桃!\t");
	if(i>=1010&&i<=120) printf("恭喜你获得琴!\t");
	if(i>=121&&i<=140) printf("恭喜你获得温迪!\t");
	if(i>=141&&i<=160) printf("恭喜你获得魈!\t");
	if(i>=161&&i<=180) printf("恭喜你获得钟离!\t");
	if(i>=181&&i<=200) printf("恭喜你获得阿贝多!\t");
	if(i>=201&&i<=220) printf("恭喜你获得刻晴!\t");
	if(i>=221&&i<=240) printf("恭喜你获得莫娜!\t");
	if(i>=241&&i<=260) printf("恭喜你获得达达利亚!\t");
	if(i>=261&&i<=280) printf("恭喜你获得七七!\t");
	if(i>=281&&i<=300) printf("恭喜你获得甘雨!\t");
	if(i>=301&&i<=400) printf("安柏+1\t");
	if(i>=401&&i<=500) printf("香菱+1\t");
	if(i>=501&&i<=600) printf("班尼特+1\t");
	if(i>=601&&i<=700) printf("辛焱+1\t");
	if(i>=701&&i<=800) printf("砂糖+1\t");
	if(i>=801&&i<=900) printf("凝光+1\t");
	if(i>=901&&i<=1000) printf("诺艾尔+1\t");
	if(i>=1001&&i<=1100) printf("丽莎+1\t");
	if(i>=1101&&i<=1200) printf("雷泽+1\t");
	if(i>=1201&&i<=1300) printf("北斗+1\t");
	if(i>=1301&&i<=1400) printf("菲谢尔+1\t");
	if(i>=1401&&i<=1500) printf("芭芭拉+1\t");
	if(i>=1501&&i<=1600) printf("行秋+1\t");
	if(i>=1601&&i<=1700) printf("凯亚+1\t");
	if(i>=1701&&i<=1800) printf("重云+1\t");
	if(i>=1801&&i<=1900) printf("DIO+1\t");
	if(i>=1901&&i<=9999) printf("三星+1\t");
	if(i==10000) printf("恭喜你获得!\n");
	if(i>=301&&i<=9999) {p++; printf("你已经%d发没出金了\n",p);}
	if(i>=1&&i<=300) {p=0;printf("你已经%d发没出金了\n",p);}
	if(p>=50) {printf("臭保底人!"); p=0; i=(rand()%1000+1)/4; card(i);x++;}
}

int main()
{
	int n=-1,i;
	while(n!=0)
	{
		printf("1单抽,2十连,0退出:");
		scanf("%d",&n);
		switch(n)
		{
		case 1:i=rand()%10000+1;card(i);break;
		case 2:for(x=0;x<10;x++) {i=rand()*rand()%10000+1;card(i);}break;
		case 0:break;
		default:printf("error!");
		}
	}
	return 0;
}








//空，荧，迪卢克，可莉，胡桃，琴，温迪，魈，钟离，阿贝多，刻晴，莫娜，达达利亚，七七，甘雨
//安柏，香菱，班尼特，辛焱，砂糖，凝光，诺艾尔，丽莎，雷泽，北斗，菲谢尔，芭芭拉，行秋，凯亚，重云，DIO