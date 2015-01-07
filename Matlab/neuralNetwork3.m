function [W1, T1, W2, T2] = neuralNetwork3()
    clear;
    tic;
    
    global Ni Nhn No alpha W1 W2 T1 T2 targets1
    Ni=9;
    Nhn=10;
    No=2;
    alpha=0.06;

    [features, targets, targets1, unknown] = readData();
    [learnData, learnTargets, testData, testTargets, controlData, controlTargets] = splitData(features,targets);
    [W1,T1,W2,T2] = fillMatrixRandom();
    toc

    succesRate=0;
    q=1;
    while succesRate < 0.9
        learn(learnData, learnTargets,q);
        succesRate = checkAantalGoed(testData,testTargets)
        q+1;
    end
    succesRate = checkAantalGoed(controlData,controlTargets)
        
    %% de Unknown raden
    %finalResults = calculateUnknown(unknown);
    
    toc
end

function [features, targets, targets1, unknown] = readData()
    features=csvread('bewijsgevonden.txt');
    unknown=csvread('unknown.txt');
    targets1=csvread('uitkomst.txt');
    targets=zeros(92,2);      %de targetspace vectorizeren
    for i=1:92
        A=targets1(i);
        targets(i,A)=1;
    end
end

function [learnData, learnTargets, testData, testTargets, controlData, controlTargets] = splitData(features,targets)
    select = randperm(92);
    learnData = features(select(1:55),:);
    learnTargets = targets(select(1:55),:);
    testData = features(select(56:75),:);
    testTargets = targets(select(56:75),:);
    controlData = features(select(76:92),:);
    controlTargets = targets(select(76:92),:);
end

function [W1,T1,W2,T2] = fillMatrixRandom()
    global Ni Nhn No
    W1 = rand(Nhn,Ni)*2-1;
    T1 = rand(1,Nhn)*2-1;
    W2 = rand(No,Nhn)*2-1;
    T2 = rand(1,No)*2-1;
end

function [succesRate] = checkAantalGoed(features, targets)
    test = 0;
    for i=1:size(features,1)    
        [x1, X1, Y1, x2, X2, Y2] = calculateOutput(features,i);
        
        [A,B]=max(Y2);
        [C,D] = max(targets(i,:));
        if B==D
            test=test+1;
        end
    end
    
    succesRate=test/size(features,1);
end

function [x1 X1 Y1 x2 X2 Y2] = calculateOutput(data,p)
    global W1 W2 T1 T2
    x1=data(p,:);        %hidden layer berekenen
    X1=x1*W1'-T1;
    Y1=1./(1+exp(-X1));

    x2=Y1;               %output layer berekenen
    X2=x2*W2'-T2;
    Y2=1./(1+exp(-X2));
end

function learn(features, targets, q)
    global Ni Nhn No W1 W2 T1 T2 alpha
    for p=1:size(features,1)
            [x1, X1, Y1, x2, X2, Y2] = calculateOutput(features,p);

            e(q,:)=targets(p,:)-Y2;  %error berekenen

            Dk = Y2.*(1-Y2).*e(q,:);

            for i=1:No %updaten w2
                for j=1:Nhn
                    W2(i,j)=W2(i,j)+alpha*Y1(j)*Dk(i);
                end
                T2(i)=T2(i)+alpha*Dk(i)*Y1(j); %updaten t2
            end

            for i=1:Nhn
                Dj(i)=Y1(i)*(1-Y1(i))*Dk*W2(:,i);
            end

            for i=1:Nhn % updatgen w1
                for j=1:Ni
                    W1(i,j)=W1(i,j)+alpha*x1(j)*Dj(i);
                end
                T1(i)=T1(i)+alpha*T1(i)*Dj(i); %updaten t1
            end
        end
end

function [finalResults] = calculateUnknown(features)
    for i=1:784
        [x1 X1 Y1 x2 X2 Y2] = calculateOutput(features,i);
        
        [A,B]=max(Y2);   % van vector naar waarde
        finalResults(i,1)=B;
    end
end