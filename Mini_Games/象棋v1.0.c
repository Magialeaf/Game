#include<stdio.h>
int main()
{
	int i,j,x=-1,y=-1,x0,y0;
	char ox,oy,oz='.';
	char a[10][9]={'c','m','x','s','a','s','x','m','c','.','.','.','.','.','.','.','.','.','.','p','.','.','.','.','.','p','.','b','.','b','.','b','.','b','.','b','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','B','.','B','.','B','.','B','.','B','.','P','.','.','.','.','.','P','.','.','.','.','.','.','.','.','.','.','C','M','X','S','A','S','X','M','C'};
	while(x!=10&&y!=10)
		{
		printf("     1 2 3 4 5 6 7 8 9\n");
		for(i=0;i<10;i++)
		{
			printf("   %d ",i);
			for(j=0;j<8;j++)
			printf("%c ",a[i][j]);
			printf("%c",a[i][8]);
			printf("\n");
		}
		printf("请输入移动棋子：");
		scanf("%d %d",&x,&y);
		printf("请输入移动位置：");
		scanf("%d %d",&x0,&y0);
		ox=a[x][y-1];
		oy=a[x0][y0-1];
		if((oy>=65&&oy<=90)||(oy>=97&&oy<=122))
			{
				a[x0][y0-1]=ox;
				a[x][y-1]=oz;
			}
		else
			{
			a[x0][y0-1]=ox;
			a[x][y-1]=oy;
			}
		}
	return 0;
}
