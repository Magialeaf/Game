#include<stdio.h>
#include<stdlib.h>
#include<conio.h>
#include<windows.h>

int position_x , position_y;  //鸟鸟位置
int enemy_x,enemy_y;  //敌人位置
int high , width;  //屏幕长度
int score;  //分
int velocity_y;  //下落速度


void startup();
void show();
int updateWithoutInput();
void updateWithInput();

void HideCursor() // 用于隐藏光标
{
CONSOLE_CURSOR_INFO cursor_info = {1, 0};  // 第二个值为0表示隐藏光标
SetConsoleCursorInfo(GetStdHandle(STD_OUTPUT_HANDLE), &cursor_info);
}

void gotoxy(int x,int y)
{
	HANDLE handle = GetStdHandle(STD_OUTPUT_HANDLE);
	COORD pos;
	pos.X = x;
	pos.Y = y;
	SetConsoleCursorPosition(handle,pos);
}

int main()
{
	int a=0;
	HideCursor();
	startup();
	while(a != 1)
	{
		show();
		a=updateWithoutInput();
		updateWithInput();
	}
	printf("游戏结束!");
	return 0;
}

void startup()
{
	high = 20;
	width = 20;
	position_x = 0;
	position_y = 0;
	enemy_x = width-1;
	enemy_y = rand()%high;
	score = 0;
	velocity_y = 1;
}

void show()
{
	int i,j;
	gotoxy(0,0);
	if((enemy_y<5)||(enemy_y>15))
		enemy_y=10;
	for(i=0;i<high-1;i++)
	{
		for(j=0;j<width;j++)
		{
			if((position_y == i) && (position_x ==j))
				printf("@");
			else if((enemy_x == j) && ((i<enemy_y) || (i>=enemy_y+5)))
				printf("*");
			else
				printf(" ");
		}
		printf("\n");
	}
	printf("您的当前得分为:%d\n",score);
}

int updateWithoutInput()
{

	if(score<5) Sleep(200);
	if(score>=5&&score<12) {printf("加速咯!");Sleep(150);}
	if(score>=12&&score<25) {printf("又加速咯!");Sleep(100);}
	if(score>=25) {printf("再加速咯!");Sleep(50);}
	enemy_x--;
	if(enemy_x<0)
	{
		enemy_x = width-1;
		enemy_y = rand()%high;
		score++;
	}
	if(position_y<high-1)
		position_y++;
	if(position_y==high-1)
		return 1;
	if((enemy_x == position_x) && ((position_y<enemy_y) || (position_y>=enemy_y+5)))
		return 1;
	return 0;
}

void updateWithInput()
{
	char input;
	if(kbhit())
	{
		input = getch();
		switch(input)
		{
			case ' ':position_y-=2;if(position_y<0) position_y = 0;break;
			default:;
		}
	} 
}