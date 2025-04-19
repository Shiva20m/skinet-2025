export type Envelope<T>=
{
    contentTypes:[],
    declaredType:string,
    formatters:[],
    statusCode:number,
    value:T, 
}