#include<stdio.h>
#include<stdlib.h>
#include<conio.h>
#include<windows.h>

int position_x , position_y;  //飞机位置
int bullet_x , bullet_y;  //子弹位置
int high , width;  //屏幕长度
int enemy_x , enemy_y;  //敌机位置
int score;  //分

void startup();
void show();
void updateWithoutInput();
int updateWithInput();

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
		updateWithoutInput();
		a=updateWithInput();
	}
	printf("退出成功!");
	return 0;
}

void startup()
{
	high = 18;
	width = 30;
	position_x = width / 2;
	position_y = high / 2;
	bullet_x = position_x;
	bullet_y = -1;
	enemy_x = 10;
	enemy_y = 0;
	score = 0;
}

void show()
{
	int i,j;
	gotoxy(0,0);
	for(i=0;i<high;i++)
	{
		for(j=0;j<width;j++)
		{
			if((position_x == j) && (position_y == i))
				printf("*");
			else if((enemy_x == j) && (enemy_y == i))
				printf("@");
			else if((bullet_x == j) && (bullet_y == i))
				printf("|");
			else
				printf(" ");
		}
		printf("\n");
	}
	printf("您的当前得分为:%d,按“P”退出游戏\n",score);
}

void updateWithoutInput()
{
	static int speed=0;
	if(speed<10)
		speed++;
	if(bullet_y>-1)
		bullet_y--;
	if(enemy_y>width)
	{
		enemy_y = 0;
		enemy_x = rand() % width;
	}
	else
	{
		if(speed==10)
		{
			enemy_y++;
			speed=0;
		}
	}
	if((bullet_x == enemy_x)&&(bullet_y == enemy_y))
	{
		score++;
		enemy_x	= rand() % width;
		enemy_y = 0;
	}
	if((position_x == enemy_x)&&(position_y == enemy_y))
	{
		score-=3;
		enemy_x	= rand() % width;
		enemy_y = 0;
	}
}

int updateWithInput()
{
	char input;
	if(kbhit())
	{
		input = getch();
		switch(input)
		{
			case 'w':position_y--;if(position_y<0) position_y = 0;break;
			case 's':position_y++;if(position_y>high-1) position_y = high-1;break;
			case 'a':position_x--;if(position_x<0) position_x = 0;break;
			case 'd':position_x++;if(position_x>width-1) position_x = width-1;break;
			case ' ':bullet_y=position_y-1;bullet_x=position_x;break;
			case 'p':return 1;break;
			default:;
		}
	}
	return 0;
}