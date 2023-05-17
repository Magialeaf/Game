#include<stdio.h>
#include<stdlib.h>
#include<windows.h>

void HideCursor()// 用于隐藏光标

{
CONSOLE_CURSOR_INFO cursor_info = {1, 0};  // 第二个值为0表示隐藏光标
SetConsoleCursorInfo(GetStdHandle(STD_OUTPUT_HANDLE), &cursor_info);
}


int main()
{
	int i,j;
	int x=5;
	int y=2;
	int velocity_x=1;
	int velocity_y=1;
	int top=1;
	int bottom=21;
	int left=1;
	int right=47;
	
	HideCursor();
	while(1)
	{
		if((y==top) || (y==bottom))
			velocity_y= -1 * velocity_y;
		if((x==left) || (x==right))
			velocity_x= -1 * velocity_x;
		x+=velocity_x;
		y+=velocity_y;
		Sleep(60);//调节延迟
		system("cls");
		for(i=0;i<y-1;i++)
		{
			for(j=0;j<right+1;j++)
				printf(" ");
			printf("|\n");
		}
		for(j=0;j<x;j++)
			printf(" ");
		printf("O");
		for(j=0;j<right-x;j++)
			printf(" ");
		printf("|\n");
		for(i=0;i<bottom-y;i++)
			{
			for(j=0;j<right+1;j++)
				printf(" ");
			printf("|\n");
			}
		for(i=0;i<right;i=i+2)
			printf("—");
	}
	return 0;
}
