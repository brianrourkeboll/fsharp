type R1 = { A : int; B : string }
type R2 = { X : int; Y : string }
type R3 = { ...R1; C : float }
type R4 = { ...R2; D : float }
type R5 = { ...R1; ...R2; E : float }

let r1 : R1 = { A = 3; B = "lol" }
let r2 : R2 = { X = 4; Y = "ha" }
let r3 : R3 = { ...r1; C = 3.14 }
let r4 : R4 = { ...r2; D = 3.14 }
let r5 : R5 = { ...r1; ...r2; E = 3.14 }