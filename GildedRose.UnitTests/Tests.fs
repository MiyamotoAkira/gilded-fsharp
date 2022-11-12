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
let ``Conjured Item quality cannot go below 0`` () =
    let item = Item.UpdateConjuredItem {
        Name = "foo"; 
        SellIn = 0; 
        Quality = 0
    }

    test <@ "foo" = item.Name @>
    test <@ -1 = item.SellIn @>
    test <@ 0 = item.Quality @>

[<Fact>]
let ``Conjured Item reduces quality at double rate after SellIn is 0`` () =
    let item = Item.UpdateConjuredItem {
        Name = "foo"; 
        SellIn = -1; 
        Quality = 4
    }

    test <@ "foo" = item.Name @>
    test <@ -2 = item.SellIn @>
    test <@ 0 = item.Quality @>

[<Fact>]
let ``Conjured Item reduces Quality By 2 and SellIn by 1`` () =
    let item = Item.UpdateConjuredItem {
        Name = "foo"; 
        SellIn = 1;
        Quality = 3
    }

    test <@ "foo" = item.Name @>
    test <@ 0 = item.SellIn @>
    test <@ 1 = item.Quality @>

[<Fact>]
let ``Appreciating Item increases Quality By 1 and SellIn by 1`` () =
    let item = Item.UpdateItem {
        Name = "Aged Brie"; 
        SellIn = 0; 
        Quality = 0
    }

    test <@ "Aged Brie" = item.Name @>
    test <@ -1 = item.SellIn @>
    test <@ 2 = item.Quality @>
    
[<Fact>]
let ``Appreciating Item's quality increases double speed after SellIn`` () =
    let item = Item.UpdateAppreciatingItem {
        Name = "Aged Brie"; 
        SellIn = -1; 
        Quality = 8
    }

    test <@ "Aged Brie" = item.Name @>
    test <@ -2 = item.SellIn @>
    test <@ 10 = item.Quality @>

[<Fact>]
let ``Appreciating Item's quality is capped at 50`` () =
    let item = Item.UpdateAppreciatingItem {
        Name = "Aged Brie"; 
        SellIn = 1; 
        Quality = 50
    }

    test <@ "Aged Brie" = item.Name @>
    test <@ 0 = item.SellIn @>
    test <@ 50 = item.Quality @>
    
[<Fact>]
let ``Appreciating Item's quality cannot increase above 50`` () =
    let item = Item.UpdateAppreciatingItem {
        Name = "Aged Brie"; 
        SellIn = 1; 
        Quality = 49
    }

    test <@ "Aged Brie" = item.Name @>
    test <@ 0 = item.SellIn @>
    test <@ 50 = item.Quality @>
    
