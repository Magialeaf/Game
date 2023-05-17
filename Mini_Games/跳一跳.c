#include<stdio.h>
#include<stdlib.h>
int main()
{
	int i,n,z=-1,x,s=0,r;
	
	printf("输入1和对面空格中间差了几个空格（0也算1个）\n");
	while(z!=0)
	{
	printf("————————————————\n");
	r=rand()%10+1;
	printf("000");
	for(i=1;i<=r;i++)
		printf(" ");
	printf("000");
	printf("\n");

	printf("010");
	for(i=1;i<=r;i++)
	printf(" ");
	printf("0 0");
	printf("\n");

	printf("000");
	for(i=1;i<=r;i++)
	printf(" ");
	printf("000");
	printf("\n");

	scanf("%d",&x);
	if(x==r+2)
	{
		s++;
		printf("恭喜你有%d分\n",s);
	}
	else 
		z=0;
	}
	printf("游戏失败!\n");

}
