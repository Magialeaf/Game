#include<stdio.h>
#include<stdlib.h>
#include<conio.h>
#include<windows.h>

int position_x , position_y;  //板板位置
int bullet_x , bullet_y;  //子弹位置
int velocity_x,velocity_y;  //子弹速度
int high , width;  //屏幕长度
int enemy_x , enemy_y;  //敌人位置
int score;  //分
int longe; //板板长度

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
	high = 18;
	width = 30;
	position_x = width / 2;
	position_y = high-1;
	bullet_x = position_x+2;
	bullet_y = high-2;
	enemy_x = 10;
	enemy_y = 0;
	score = 0;
	longe = 5;
	velocity_x = 1;
	velocity_y = 1;
}

void show()
{
	int i,j;
	gotoxy(0,0);
	for(i=0;i<high-1;i++)
	{
		for(j=0;j<width;j++)
		{
			if((enemy_x == j) && (enemy_y == i))
				printf("@");
			else if((bullet_x == j) && (bullet_y == i))
				printf("O");
			else
				printf(" ");
		}
		printf("|\n");
	}
	for(i=0;i<width-longe+1;i++)
		if(position_x == i)
			for(j=0;j<longe;j++)
				printf("*");
		else
			printf(" ");
		printf("|\n");
	printf("您的当前得分为:%d\n",score);
}

int updateWithoutInput()
{
	static int speed=0;
	if(speed<10)
		speed++;
	if(speed==10)
		{
		if(bullet_y==0)
			velocity_y= -1 * velocity_y;
		if((bullet_x==0) || (bullet_x== width-1))
			velocity_x= -1 * velocity_x;
		if((bullet_y==high-2) && (bullet_x>=position_x) && (bullet_x<position_x+5))
			velocity_y= -1 * velocity_y;
		if(bullet_y==high)
			return 1;
		bullet_x+=velocity_x;
		bullet_y+=velocity_y;
			speed=0;
		}
	if((bullet_x == enemy_x)&&(bullet_y == enemy_y))
	{
		score++;
		enemy_x	= rand() % width;
		enemy_y = 0;
	}
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
			case 'a':position_x--;if(position_x<0) position_x = 0;break;
			case 'd':position_x++;if(position_x>width-5) position_x = width-5;break;

			default:;
		}
	}
	return 0;
} 