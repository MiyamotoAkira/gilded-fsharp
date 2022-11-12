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
    
    let AgeItem item =
        { item with SellIn  = (item.SellIn - 1) }
    
    let ReduceQuality amount item =
        if item.Quality > 0 then
            { item with Quality = (item.Quality - amount) }
        else
            item
            
    let ReduceExpiredItemQuality amount item =
        if item.SellIn < 0 && item.Quality > 0 then
            { item with Quality   = (item.Quality  - amount) }
        else
            item

    let ReduceNormalQuality = ReduceQuality 1
    let ReduceExpiredNormalItemQuality = ReduceExpiredItemQuality 1
    
    let ReduceConjuredQuality = ReduceQuality 2 
    let ReduceExpiredConjuredItemQuality = ReduceExpiredItemQuality 2

    let IncreaseAppreciatingItemQuality item =
            if item.Quality < 50 then
                { item with Quality = (item.Quality + 1) } 
            else
                item
    
    let IncreaseExpiredAppreciatingItemQuality item =
            if item.SellIn < 0 then
                if item.Quality < 50 then
                    { item with Quality   = (item.Quality + 1) }
                else
                    item
            else
                item
        
    let UpdateNormalItem oldItem =
        oldItem |> AgeItem |> ReduceNormalQuality |> ReduceExpiredNormalItemQuality 

    let UpdateConjuredItem oldItem =
        oldItem |> AgeItem |> ReduceConjuredQuality |> ReduceExpiredConjuredItemQuality 

    let UpdateAppreciatingItem item =
            item |> AgeItem |> IncreaseAppreciatingItemQuality |> IncreaseExpiredAppreciatingItemQuality 
  
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
  
type GildedRose(items : IList<Item>) =
    let mutable Items = Seq.toList items

    member this.UpdateQuality() =
        
        let updateItem item =
            match item with
                | i when i.Name = "Aged Brie" -> Item.UpdateAppreciatingItem i
                | i when i.Name = "Sulfuras, Hand of Ragnaros" ->  Item.UpdateItem i
                | i when i.Name = "Backstage passes to a TAFKAL80ETC concert" ->  Item.UpdateItem i
                | i when i.Name = "Conjured Mana Cake" -> Item.UpdateConjuredItem i
                | i -> Item.UpdateNormalItem i
       
        Items <- List.map updateItem Items
        ()
    
    member this.PrintMe() =
        List.iter (fun item -> printfn "%s, %d, %d" item.Name item.SellIn item.Quality) Items
        
    
module Program =
    [<EntryPoint>]
    let main argv =
        printfn "OMGHAI!"
        let Items = new List<Item>()
        Items.Add({Name = "+5 Dexterity Vest"; SellIn = 10; Quality = 20})
        Items.Add({Name = "Aged Brie"; SellIn = 2; Quality = 0})
        Items.Add({Name = "Elixir of the Mongoose"; SellIn = 5; Quality = 7})
        Items.Add({Name = "Sulfuras, Hand of Ragnaros"; SellIn = 0; Quality = 80})
        Items.Add({Name = "Sulfuras, Hand of Ragnaros"; SellIn = -1; Quality = 80})
        Items.Add({Name = "Backstage passes to a TAFKAL80ETC concert"; SellIn = 15; Quality = 20})
        Items.Add({Name = "Backstage passes to a TAFKAL80ETC concert"; SellIn = 10; Quality = 49})
        Items.Add({Name = "Backstage passes to a TAFKAL80ETC concert"; SellIn = 5; Quality = 49})
        Items.Add({Name = "Conjured Mana Cake"; SellIn = 3; Quality = 6})

        let app = new GildedRose(Items)
        for i = 0 to 30 do
            printfn "-------- day %d --------" i
            printfn "name, sellIn, quality"
            app.PrintMe()
            printfn ""
            app.UpdateQuality()
        0 