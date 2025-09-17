type R1 =
    { A : int
      B : string }
    member _.Lol = 99

type R2 = { ...R1; C : string }

let r1 = { A = 3; B = "4" }
let r2 = { A = 5; B = "6"; C = "haha" }

let r1AndR2 = {| ...r1; ...r2 |}