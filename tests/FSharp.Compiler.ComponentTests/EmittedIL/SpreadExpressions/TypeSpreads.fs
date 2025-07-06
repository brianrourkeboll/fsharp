type R1 = { A : int; B : string }
type R2 = { X : int; Y : string }
type R3 = { ...R1; C : float }
type R4 = { ...R2; D : float }
type R5 = { ...R1; ...R2; E : float }

let _ : R3 = { A = 3; B = "lol"; C = 3.14 }
let _ : R4 = { X = 3; Y = "lol"; D = 3.14 }
let _ : R5 = { A = 3; B = "lol"; X = 3; Y = "lol"; E = 3.14 }