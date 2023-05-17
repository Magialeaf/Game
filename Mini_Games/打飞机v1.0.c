#include<stdio.h>
#include<stdlib.h>
#include<conio.h>
#include <windows.h>

void HideCursor() // 用于隐藏光标
{
CONSOLE_CURSOR_INFO cursor_info = {1, 0};  // 第二个值为0表示隐藏光标
SetConsoleCursorInfo(GetStdHandle(STD_OUTPUT_HANDLE), &cursor_info);
}

int main()
{
	int i,j;
	int x = 5;
	int y = 20;
	int isFired = 0;
	int length;
	int mark = 0;
	int isKilled = 1;
	int left = 0;
	int right = 100;
	int top = 5;
	int bottom = 50;
	char input;

	HideCursor();
	while(input != 'p')
	{
		system("cls");
		if(isKilled == 1)
		{
			length = rand()/1000+5;
			isKilled = 0;
		}
		for(i=0;i<length;i++)
			printf(" ");
		printf("+");
		for(i=0;i<right-length;i++)
			printf(" ");
		printf("|\n");
		if(isFired == 1)
		{
			for(i=0;i<y;i++)
			{
				for(j=0;j<x;j++)
					printf(" ");
				printf("  |");
				for(j=0;j<right-x-2;j++)
					printf(" ");
				printf("|\n");
			}
			if(length==x+2)
			{mark++;isKilled=1;}
			isFired=0;
		}
		else
		{
			for(i=0;i<y;i++)
			{
				for(j=0;j<right+1;j++)
					printf(" ");
				printf("|\n");
			}

		}
		for(i=0;i<x;i++)
			printf(" ");
			printf("  *  ");
		for(j=0;j<right-x-4;j++)
			printf(" ");
				printf("|\n");
		for(i=0;i<x;i++)
			printf(" ");
			printf("*****");
		for(j=0;j<right-x-4;j++)
			printf(" ");
				printf("|\n");
		for(i=0;i<x;i++)
			printf(" ");
			printf(" * * ");
		for(j=0;j<right-x-4;j++)
			printf(" ");
				printf("|\n");
		printf("ad左右移动,空格攻击,攻击“+”,按p退出游戏\n");
		printf("您的当前得分为:%d\n",mark);

		input = getch();
		if(input == 'p')
			return 0;
		switch(input)
		{
		/*case 'w':y--;break;
		case 's':y++;break;*/
		case 'a':x--;if(x==-1) x=96;break;
		case 'd':x++;if(x==97) x=0;break;
		default:;
		}
		if(input==' ')
			isFired=1;
	}
return 0;
}