#define _CRT_SECURE_NO_WARNINGS
#include<stdio.h>
#include<string.h>
#include <stdlib.h>
#include <time.h>
#include<windows.h>

int all[52][2];
int use[52] = { 0 }; //0未发 1发了
int money = 20;//金钱
int bei = 1;//倍率

typedef struct player
{
	int ID[3];
	int picture[3];
	int value_ID;
	int value_rank;
	int value_picture;
}new_player;
//————————————————————————————————————***————————————————————————————————————
//初始化牌堆
void new_card();
//次方
int pow(int num, int p);
//展示牌
void show(int num, new_player* player);
//发牌
new_player start();
//排序
void sort(new_player* player);
//总价值判断
void value(new_player* player);
//比较
bool cmp(new_player* player1, new_player* player2);
//胜利判断
void win(new_player* player1, new_player* player2);
//展示牌型
void show_type(int num, new_player* player);
//数字->花色
void picture(int i, char* res);
//数字->牌号
void map(int i, char* res);
//玩家选择胜方
int AI(new_player* player1,new_player* player2);
//玩家获胜
void playerwin(new_player* player1, new_player* player2,int co);
//————————————————————————————————————***————————————————————————————————————
//数字->花色
void picture(int i, char* res)
{
	switch (i)
	{
	case 1:strcpy(res, "spade"); break; //黑桃
	case 10:strcpy(res, "club"); break;  //梅花
	case 100:strcpy(res, "diamond"); break;  //方块
	case 1000:strcpy(res, "heart"); break;  //红桃
	default:break;
	}
}
//数字->牌号
void map(int i, char* res)
{
	switch (i)
	{
	case 2:strcpy(res, "2"); break;
	case 3:strcpy(res, "3"); break;
	case 4:strcpy(res, "4"); break;
	case 5:strcpy(res, "5"); break;
	case 6:strcpy(res, "6"); break;
	case 7:strcpy(res, "7"); break;
	case 8:strcpy(res, "8"); break;
	case 9:strcpy(res, "9"); break;
	case 10:strcpy(res, "10"); break;
	case 11:strcpy(res, "J"); break;
	case 12:strcpy(res, "Q"); break;
	case 13:strcpy(res, "K"); break;
	case 14:strcpy(res, "A"); break;
	default:break;
	}
}
//初始化牌堆
void new_card()
{
	int count = 0;
	for (int i = 0; i < 4; i++)
		for (int j = 0; j < 13; j++)
		{
			all[count][0] = 2 + j;
			all[count][1] = pow(10,i);
			count++;
		}
	for (int i = 0; i < 52; i++)
		use[i] = 0;
}
//次方
int pow(int num, int p)
{
	int res = 1;
	for (int i = 0; i < p; i++)
		res *= num;
	return res;
}
//展示牌
void show(int num, new_player* player)
{
	char ID[3];
	char pic[10];
	for (int i = 0; i < 3; i++)
	{
		map(player->ID[i], ID);
		picture(player->picture[i], pic);
		printf("玩家%d的第%d张牌为%s,花色为%s\n", num, i + 1, ID, pic);
	}
	printf("\n");
}
//发牌
new_player start()
{
	int rand_num;
	new_player player;
	srand((unsigned)time(NULL));
	for (int i = 0; i < 3; i++)
	{
		do 
		{
			rand_num = rand() % 52;
		} while (use[rand_num] == 1);
		player.ID[i] = all[rand_num][0];
		player.picture[i] = all[rand_num][1];
		use[rand_num] = 1;
	}
	return player;
}
//排序
void sort(new_player* player)
{
	int temp;
	int pic;
	for(int i =0;i<3;i++)
		for(int j=i;j<3;j++)
			if (player->ID[i] > player->ID[j])
			{
				temp = player->ID[i];
				player->ID[i] = player->ID[j];
				player->ID[j] = temp;
				pic = player->picture[i];
				player->picture[i] = player->picture[j];
				player->picture[j] = pic;
			}
}
//总价值判断
void value(new_player* player)
{
	player->value_picture = player->picture[0] + player->picture[1] + player->picture[2];
	if (player->ID[0] == player->ID[1] && player->ID[1] == player->ID[2]) //豹子
	{
		player->value_rank = 10;
		player->value_ID = player->ID[0];
	}
	else if ((player->ID[0] + 1 == player->ID[1]) && (player->ID[1] + 1 == player->ID[2]) && player->picture[0] == player->picture[1] && player->picture[1] == player->picture[2])//同花顺
	{
		player->value_rank = 9;
		player->value_ID = player->ID[2];
	}
	else if (player->picture[0] == player->picture[1] && player->picture[1] == player->picture[2])//金花
	{
		player->value_rank = 8;
		player->value_ID = player->ID[0] + player->ID[1] * 10 + player->ID[2] * 100;
	}
	else if ((player->ID[0] + 1 == player->ID[1]) && (player->ID[1] + 1 == player->ID[2]))//顺子
	{
		player->value_rank = 7;
		player->value_ID = player->ID[2];
	}
	else if (player->ID[0] == player->ID[1] || player->ID[1] == player->ID[2])//对子
	{
		player->value_rank = 6;
		if (player->ID[0] == player->ID[1])
			player->value_ID = player->ID[0] * 100 + player->ID[1] * 100 + player->ID[2];
		else if(player->ID[1] == player->ID[2])
			player->value_ID = player->ID[0] + player->ID[1] * 100 + player->ID[2] * 100;
	}
	else//单张
	{
		player->value_rank = 5;
		player->value_ID = player->ID[0] + player->ID[1] * 10 + player->ID[2] * 100;
	}
}
//比较
bool cmp(new_player* player1, new_player* player2)
{
	bool res;
	if (player1->value_rank > player2->value_rank)
		res = true;
	else if(player1->value_rank < player2->value_rank)
		res = false;
	else if (player1->value_ID > player2->value_ID)
		res = true;
	else if (player1->value_ID < player2->value_ID)
		res = false;
	else if(player1->value_picture > player2->value_picture)
		res = true;
	else if (player1->value_picture < player2->value_picture)
		res = false;
	return res;
}

void win(new_player* player1, new_player* player2)
{
	show_type(1,player1);
	show_type(2,player2);
	if (cmp(player1, player2) == true)
		printf("玩家1获胜");
	else
		printf("玩家2获胜");
}

void show_type(int num,new_player* player)
{
	switch (player->value_rank)
	{
	case 5:printf("玩家%d的牌型为：单张\n",num); break;
	case 6:printf("玩家%d的牌型为：对子\n", num); break;
	case 7:printf("玩家%d的牌型为：顺子\n", num); break;
	case 8:printf("玩家%d的牌型为：金花\n", num); break;
	case 9:printf("玩家%d的牌型为：同花顺\n", num); break;
	case 10:printf("玩家%d的牌型为：豹子\n", num); break;
	}
}
//管理员版
void admin()
{
	int n = 1;
	do
	{
		new_player player1;
		new_player player2;
		new_card();
		player1 = start();
		player2 = start();
		show(1, &player1);
		show(2, &player2);
		sort(&player1);
		sort(&player2);
		value(&player1);
		value(&player2);
		win(&player1, &player2);
		//printf("\ninput（输入0退出，其他数字继续）:");
		//scanf("%d",&n);
		Sleep(8000);
		system("cls");
	} while (n != 0);

}
//玩家1开挂版
void G()
{
	int n = 1;
	do
	{
		new_player player1;
		new_player player2;
		new_card();
		//player1 = start();
		player2 = start();
		player1.ID[0] = 14;
		player1.ID[1] = 14;
		player1.ID[2] = 14;
		player1.picture[0] = 1000;
		player1.picture[1] = 100;
		player1.picture[2] = 10;
		show(1, &player1);
		show(2, &player2);
		sort(&player1);
		sort(&player2);
		value(&player1);
		value(&player2);
		win(&player1, &player2);
		//printf("\ninput（输入0退出，其他数字继续）:");
		//scanf("%d",&n);
		Sleep(8000);
		system("cls");
	} while (n != 0);
}

//玩家选择版
void player()
{
	int n = 1;
	int co;
	do
	{
		system("cls");
		new_player player1;
		new_player player2;
		new_card();
		player1 = start();
		show(1, &player1);
		player2 = start();
		sort(&player1);
		sort(&player2);
		value(&player1);
		value(&player2);
		co = AI(&player1, &player2);
		system("cls");
		playerwin(&player1, &player2, co);
		Sleep(8000);
		system("cls");		
		printf("\ninput（输入0退出，其他数字继续）:");
		scanf("%d", &n);
	} while (n != 0);
}
void main()
{
	player();
}

int AI(new_player* player1, new_player* player2)
{
	int co;
	int rand_num;
	bei = 1;
	srand((unsigned)time(NULL));
	printf("丢损失倍数金钱，跟倍数翻倍，开比大小后获得或损失双倍金钱\n");
	printf("*你当前的金钱为%d*", money);
	do
	{
		printf("\n————————————————————\n当前倍数为:%d", bei);
		printf("你选择：\n1.开  2.跟   0.丢\ninput:", money);
		scanf("%d", &co);
		rand_num = rand() % (100) + 1;
		if (co == 2 && rand_num <= (player2->value_rank * 10))
		{
			printf("{玩家2选择了跟}");
			bei *= 2;
		}
		else if(co == 2 && rand_num > (player2->value_rank * 10))
		{
			printf("{玩家2选择了开}");
			co = 1;
		}
	} while (co == 2);
	Sleep(3000);
	return co;
}

void playerwin(new_player* player1, new_player* player2,int co)
{
	show(1, player1);
	show(2, player2);
	show_type(1, player1);
	show_type(2, player2);
	printf("[当前倍率为:%d]\n",bei);
	if (co == 1)
	{
		if (cmp(player1, player2) == TRUE)
		{
			printf("<玩家1获胜，你获得了%d元>\n", bei * 2);
			money += bei * 2;
		}
		else
		{
			printf("<玩家2获胜，你损失了%d元>\n", bei * 2);
			money -= bei * 2;
		}
	}
	else
	{
		printf("<玩家2获胜，你损失了%d元>\n", bei);
		money -= bei;
	}
}