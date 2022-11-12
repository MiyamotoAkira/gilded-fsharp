module GildedRose.UnitTests

open GildedRose
open System.Collections.Generic
open Xunit
open Swensen.Unquote

[<Fact>]
let ``My test`` () =
    let Items = new List<BetterItems>()  
    Items.Add(NormalItem {Name = "foo"; SellIn = 0; Quality = 0})
    let app = new GildedRose(Items)
    app.UpdateQuality()
    match Items[0] with
    | NormalItem i -> test <@ "foo" = i.Name @>
    | _ -> ()