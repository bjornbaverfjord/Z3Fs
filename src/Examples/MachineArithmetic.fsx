﻿#I "../FsZ3/bin/Debug"
#r "Microsoft.Z3"
#r "FsZ3"

open Microsoft.Z3
open Microsoft.Z3.Bool
open Microsoft.Z3.Int
open Microsoft.Z3.BitVec

// Reference from http://rise4fun.com/Z3Py/tutorialcontent/guide#h24

let bitvecExample1() =
    let x = BitVec("x", 16u)
    let y = BitVec("y", 16u)
    printfn "%O" (x + 2)
    // Internal representation
    printfn "%O" (x + 2).Expr

    // -1 is equal to 65535 for 16-bit integers 
    printfn "%O" <| Z3.Simplify(x + y - 1)

    // Create bit-vector constants
    let a = BitVecVal(-1, 16u)
    let b = BitVecVal(65535, 16u)
    printfn "%O" <| Z3.Simplify(a =. b)

    let c = BitVecVal(-1, 32u)
    let d = BitVecVal(65535, 32u)
    // -1 is not equal to 65535 for 32-bit integers 
    printfn "%O" <| Z3.Simplify(c =. d)

let bitvecExample2() =
    // Create two bit-vectors of size 32
    let x = BitVec("x", 32u)
    let y = BitVec("y", 32u)

    Z3.Solve(x + y =. 2, x >. 0, y >. 0) |> ignore

    // Bit-wise operators
    // &&& bit-wise and
    // ||| bit-wise or
    // ~~~ bit-wise not
    Z3.Solve(x &&& y =. ~~~y) |> ignore

    Z3.Solve(x <. 0) |> ignore

    // using unsigned version of < 
    Z3.Solve(x <~ 0)

let bitvecExample3() =
    // Create two bit-vectors of size 32
    let x = BitVec("x", 32u)
    let y = BitVec("y", 32u)

    Z3.Solve(x >>> 2 =. 3) |> ignore
    Z3.Solve(x <<< 2 =. 3) |> ignore
    Z3.Solve(x <<< 2 =. 24)

#time "on";;

let res01 = bitvecExample1();;
let res02 = bitvecExample2();;
let res03 = bitvecExample3();;
