module GildedRose.UnitTests

open GildedRose
open System.Collections.Generic
open Xunit
open Swensen.Unquote

[<Fact>]
let ``NormalItem quality cannot go below 0`` () =
    let item = Item.UpdateNormalItem {
        Name = "foo"; 
        SellIn = 0; 
        Quality = 0
    }

    test <@ "foo" = item.Name @>
    test <@ -1 = item.SellIn @>
    test <@ 0 = item.Quality @>

[<Fact>]
let ``NormalItem reduces quality at double rate after SellIn is 0`` () =
    let item = Item.UpdateNormalItem {
        Name = "foo"; 
        SellIn = -1; 
        Quality = 2
    }

    test <@ "foo" = item.Name @>
    test <@ -2 = item.SellIn @>
    test <@ 0 = item.Quality @>


[<Fact>]
let ``NormalItem reduces Quality By 1 and SellIn by 1`` () =
    let item = Item.UpdateNormalItem {
        Name = "foo"; 
        SellIn = 1;
        Quality = 1
    }

    test <@ "foo" = item.Name @>
    test <@ 0 = item.SellIn @>
    test <@ 0 = item.Quality @>

[<Fact>]
let ``Apprecitating Item increases Quality By 1 and SellIn by 1`` () =
    let item = Item.UpdateItem {
        Name = "Aged Brie"; 
        SellIn = 0; 
        Quality = 0
    }

    test <@ "Aged Brie" = item.Name @>
    test <@ -1 = item.SellIn @>
    test <@ 2 = item.Quality @>