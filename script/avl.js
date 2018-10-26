var svgNS = "http://www.w3.org/2000/svg";
var ctr = 0;
var ctrLines=0;
function createCircle(y,x,ctr)
{
    var myCircle = document.createElementNS(svgNS,"circle"); //to create a circle. for rectangle use "rectangle"
    myCircle.setAttributeNS(null,"id","node" + ctr);
    myCircle.setAttributeNS(null,"cx",x);
    myCircle.setAttributeNS(null,"cy",y);
    myCircle.setAttributeNS(null,"r",18);
    myCircle.setAttributeNS(null,"fill","black");
    myCircle.setAttributeNS(null,"stroke","none");
    document.getElementById("nodes").appendChild(myCircle);
}
function createLine(x1,y1,x2,y2,ctrLines)
{
    var myLine = document.createElementNS(svgNS,"line");
    myLine.setAttributeNS(null,'id',"line"+ctrLines);
    myLine.setAttribute('x1',x1);
    myLine.setAttribute('x2',x2);
    myLine.setAttribute('y1',y1);
    myLine.setAttribute('y2',y2);
    myLine.setAttribute('stroke','rgb(0,0,0)');
    myLine.setAttribute('stroke-width',2);
    document.getElementById("lines").appendChild(myLine);
}
function drawString(x,y,val)
{
    var myString = document.createElementNS(svgNS,"text");
    myString.setAttributeNS(null,'id','value'+ctr);
    myString.setAttribute('x',x);
    myString.setAttribute('y',y);
    myString.setAttribute('fill','white');
    myString.textContent = val;
    myString.innerHTML = val;
    document.getElementById("text").appendChild(myString);
}
class Node {
    constructor(data,id,ctrLines) {
        this.data = data;
        this.left = null;
        this.right = null;
        this.id =null;
        this.idline=null;
    }
}


class Bst {
    constructor() {
        this.root = null;
    }

    insert(data, head,kali,bagi,tinggi) {
        if (head === null) {

            ctr++;
            this.root = new Node(data,"node"+ctr,"line"+ctrLines);
            createCircle(34,200,ctr);
            drawString(484,34,data);
            var complete = anime({
                targets: '#node'+ctr,
                translateX: 284,
                complete: function(anim) {
                    document.getElementById("node"+ctr).setAttribute("cx",484);
                    document.getElementById("node"+ctr).style.transform="";
                }
            });


        }else if (data < head.data) {
            // if left is null insert node here
            if (head.left === null) {
                ctr++;
                ctrLines++;
                head.left = new Node(data,"node"+ctr,"line"+ctrLines);
                createLine(484/bagi *kali+1,tinggi,(484/(bagi*2) )*((kali*2)-1),tinggi+50,ctrLines ); 
                drawString((484/(bagi*2) )*((kali*2)-1),tinggi+50 ,data ) ;
                createCircle(tinggi+50,(484/(bagi*2) )*((kali*2)-1), ctr );
                var complete = anime({
                    targets: '#node'+ctr,
                    translateX: 100,
                    complete: function(anim) {

                        document.getElementById("node"+ctr).style.transform="";
                    }
                });
            }
            else{
                this.insert(data, head.left,(kali*2)-1,bagi*2,tinggi+50);
            }
            // if the data is more than the node
            // data move right of the tree
        } else {
            // if right is null insert node here
            if (head.right === null) {
                ctr++;
                ctrLines++;
                head.right = new Node(data,"node"+ctr,"line"+ctrLines);
                createLine(484/bagi *kali+1,tinggi,(484/(bagi*2) )*((kali*2)+1),tinggi+50,ctrLines); 
                drawString((484/(bagi*2))*((kali*2)+1),tinggi+50,data) ;
                createCircle(tinggi+50,(484/(bagi*2))*((kali*2)+1),ctr );
                
                var complete = anime({
                    targets: '#node'+ctr,
                    translateX: 100,
                    complete: function(anim) {

                        document.getElementById("node"+ctr).style.transform="";
                    }
                });

            }
            else{
                this.insert(data, head.right,(kali*2)+1,bagi*2,tinggi+50);
            }

        }

    }

    inorder(node) {
        if (node !== null) {
            this.inorder(node.left);
            console.log(node.data);
            this.inorder(node.right);
        }
    }
    
    
    inorderRedraw(node,kali,bagi,tinggi){
        if (node !== null) {
            this.inorderRedraw(node.left,(kali*2)-1,bagi*2,tinggi+50);
            ctr++;
            console.log(node.data);
            node.idline=ctrLines;
            node.id=ctr;
            createCircle(tinggi,484/bagi *kali+1,ctr );
            drawString(484/bagi *kali+1,tinggi,node.data+ "") ;
            if (node.left!= null)
            {
                ctrLines++;
                createLine(484/bagi *kali+1,tinggi,(484/(bagi*2) )*((kali*2)-1),tinggi+50,ctrLines ); 
            }
            if (node.right!= null)
            {
                ctrLines++;
                createLine(484/bagi *kali+1,tinggi,(484/(bagi*2) )*((kali*2)+1),tinggi+50,ctrLines); 
            }
            
            this.inorderRedraw(node.right,(kali*2)+1,bagi*2,tinggi+50);
        }
        
    }
    redraw(node){
        ctrLines=0;
        ctr=0;
            document.getElementById("nodes").innerHTML="";
            document.getElementById("lines").innerHTML="";
            document.getElementById("text").innerHTML="";
            this.inorderRedraw(tree.root,1,1,34);
            
    }
    
}
var tree= new Bst();
function insertNode(){
    tree.insert(parseInt(document.getElementById("insertVal").value),tree.root,1,1,34 );
}
function redraw(){
    tree.redraw(tree.root);
}


//tree.insert(7,tree.root,1,1,34);
/*
tree.insert(2,tree.root,1,1,34);
tree.insert(3,tree.root,1,1,34);
tree.insert(4,tree.root,1,1,34);
tree.insert(5,tree.root,1,1,34);
tree.insert(6,tree.root,1,1,34);
tree.insert(13,tree.root,1,1,34);
tree.insert(11,tree.root,1,1,34);
tree.insert(14,tree.root,1,1,34);
*/
