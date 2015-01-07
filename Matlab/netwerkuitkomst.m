function [ B ] = netwerkuitkomst( A )
%UNTITLED3 Summary of this function goes here
%   Detailed explanation goes here

global W1 W2 T1 T2
    x1=A;        %hidden layer berekenen
    X1=x1*W1'-T1;
    Y1=1./(1+exp(-X1));

    x2=Y1;               %output layer berekenen
    X2=x2*W2'-T2;
    Y2=1./(1+exp(-X2));
    
    [A,B]=max(Y2);   % van vector naar waarde
     
end

