function log(message){
    console.log(message);
}
var msg="hello";
log(msg);

let a:number;
let b:boolean;
let c: string;
let d: any;
let e: number[]=[];
let f: any[]=[1,true,'hello'];

const ColorRed=0;

enum Color {Red, Green, Blue=2 };
let backgroundcolor=Color.Blue;

let msg1;
msg1='abc';
let A=(<string>msg1).endsWith('c');
let B=(msg as string).endsWith('c'); 

let Z= function(x){
    console.log("X");
}
Z("d");

let Zz=()=>{console.log('Xz');};
Zz();
