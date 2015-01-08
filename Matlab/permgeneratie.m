
vector=[0 0 1 1 1 1 1 1 1];
perm = perms(vector);
uitkomst=unique(perm,'rows');
csvwrite('bla.txt',uitkomst);