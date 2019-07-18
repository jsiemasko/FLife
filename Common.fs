namespace FLife.Common
open System

[<AutoOpen>]
module StringHelpers =
    ///<summary>Curried version of String.join</summary>
    let join (seperator:String) (values:String[]) = String.Join(seperator, values)