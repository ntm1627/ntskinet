let data:any;

data='42';
data=10;

//Interface  definition, it is very important for type safety
//you will call the property simply and you will also get intellisense
interface ICar {
   color:string
   model:string
   stopSpeed ?: number //nullable, otherwise required in car1 as well
 };

 const car1: ICar=
 {
   color:'blue',
   model:'bmw'
 };

 const car2: ICar={
   color:'red',
   model:'Toyota',
   stopSpeed:100
 }

 console.log(car1.color);

 const Multiply1 =(x:number, y:number):number=> //a function returning a number/but it is optional
 {
   return x*y;
 };

 const Multiply2 =(x:number, y:number):string=> //a function returning a void/but it is optional
 {
   return (x*y).toString();
 };
