namespace GildedRose

open System.Collections.Generic

type Item = { Name: string; SellIn: int; Quality: int }

type BetterItems =
    | NormalItem of Item
    | AppreciatingItem of Item
    | LegendaryItem of Item
    | ScalpableItem of Item
    | ConjuredItem of Item

module Item =
    let UpdateNormalItem item1 =
            let mutable item = item1
            if item.Name <> "Aged Brie" && item.Name <> "Backstage passes to a TAFKAL80ETC concert" then
                if item.Quality > 0 then
                    if item.Name <> "Sulfuras, Hand of Ragnaros" then
                        item <- { item with Quality = (item.Quality - 1) } 
            else
               if item.Quality < 50 then
                    item <- { item with Quality = (item.Quality + 1) } 
                    if item.Name = "Backstage passes to a TAFKAL80ETC concert" then
                        if item.SellIn < 11 then
                            if item.Quality < 50 then
                                item <- { item with Quality = (item.Quality + 1) } 
                        if item.SellIn < 6 then
                            if item.Quality < 50 then
                                item <- { item with Quality = (item.Quality + 1) } 
            if item.Name <> "Sulfuras, Hand of Ragnaros" then                 
                item <- { item with SellIn  = (item.SellIn - 1) } 
            if item.SellIn < 0 then
                if item.Name <> "Aged Brie" then
                    if item.Name <> "Backstage passes to a TAFKAL80ETC concert" then
                        if item.Quality > 0 then
                            if item.Name <> "Sulfuras, Hand of Ragnaros" then
                                item <- { item with Quality   = (item.Quality  - 1) } 
                    else
                        item <- { item with Quality   = (item.Quality  - item.Quality) } 
                else
                    if item.Quality < 50 then
                        item <- { item with Quality   = (item.Quality + 1) }
            item

    let UpdateItem item1 =
            let mutable item = item1
            if item.Name <> "Aged Brie" && item.Name <> "Backstage passes to a TAFKAL80ETC concert" then
                if item.Quality > 0 then
                    if item.Name <> "Sulfuras, Hand of Ragnaros" then
                        item <- { item with Quality = (item.Quality - 1) } 
            else
               if item.Quality < 50 then
                    item <- { item with Quality = (item.Quality + 1) } 
                    if item.Name = "Backstage passes to a TAFKAL80ETC concert" then
                        if item.SellIn < 11 then
                            if item.Quality < 50 then
                                item <- { item with Quality = (item.Quality + 1) } 
                        if item.SellIn < 6 then
                            if item.Quality < 50 then
                                item <- { item with Quality = (item.Quality + 1) } 
            if item.Name <> "Sulfuras, Hand of Ragnaros" then                 
                item <- { item with SellIn  = (item.SellIn - 1) } 
            if item.SellIn < 0 then
                if item.Name <> "Aged Brie" then
                    if item.Name <> "Backstage passes to a TAFKAL80ETC concert" then
                        if item.Quality > 0 then
                            if item.Name <> "Sulfuras, Hand of Ragnaros" then
                                item <- { item with Quality   = (item.Quality  - 1) } 
                    else
                        item <- { item with Quality   = (item.Quality  - item.Quality) } 
                else
                    if item.Quality < 50 then
                        item <- { item with Quality   = (item.Quality + 1) }
            item
  
type GildedRose(items:IList<BetterItems>) =
    let Items = items

    member this.UpdateQuality() =
        for i = 0 to Items.Count - 1 do
            Items.[i] <-
                match Items[i] with
                | NormalItem (i) -> NormalItem (Item.UpdateItem i)
                | AppreciatingItem (i) -> AppreciatingItem (Item.UpdateItem i)
                | LegendaryItem (i) -> LegendaryItem (Item.UpdateItem i)
                | ScalpableItem (i) -> ScalpableItem (Item.UpdateItem i)
                | ConjuredItem (i) -> ConjuredItem (Item.UpdateItem i)
        ()
    


module Program =
    [<EntryPoint>]
    let main argv =
        printfn "OMGHAI!"
        let Items = new List<BetterItems>()
        Items.Add(NormalItem {Name = "+5 Dexterity Vest"; SellIn = 10; Quality = 20})
        Items.Add(AppreciatingItem {Name = "Aged Brie"; SellIn = 2; Quality = 0})
        Items.Add(NormalItem {Name = "Elixir of the Mongoose"; SellIn = 5; Quality = 7})
        Items.Add(LegendaryItem {Name = "Sulfuras, Hand of Ragnaros"; SellIn = 0; Quality = 80})
        Items.Add(LegendaryItem {Name = "Sulfuras, Hand of Ragnaros"; SellIn = -1; Quality = 80})
        Items.Add(ScalpableItem {Name = "Backstage passes to a TAFKAL80ETC concert"; SellIn = 15; Quality = 20})
        Items.Add(ScalpableItem {Name = "Backstage passes to a TAFKAL80ETC concert"; SellIn = 10; Quality = 49})
        Items.Add(ScalpableItem {Name = "Backstage passes to a TAFKAL80ETC concert"; SellIn = 5; Quality = 49})
        Items.Add(ConjuredItem {Name = "Conjured Mana Cake"; SellIn = 3; Quality = 6})

        let app = new GildedRose(Items)
        for i = 0 to 30 do
            printfn "-------- day %d --------" i
            printfn "name, sellIn, quality"
            for j = 0 to Items.Count - 1 do
                 let item =
                     match Items[j] with
                        | NormalItem (i) -> i
                        | AppreciatingItem (i) -> i
                        | LegendaryItem (i) -> i
                        | ScalpableItem (i) -> i
                        | ConjuredItem (i) -> i
                 printfn "%s, %d, %d" item.Name item.SellIn item.Quality
            printfn ""
            app.UpdateQuality()
        0 