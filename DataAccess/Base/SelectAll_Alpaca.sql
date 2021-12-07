select 
	Alpaca.Id as alpaca_id, 
	Alpaca.Name as alpaca_name,
	Alpaca.Weight as alpaca_weight,
	Alpaca.Color as alpaca_color,
	Farm.Id as farm_id,
	Farm.Name as farm_name,
	Farm.Multiplier as farm_multiplier
from Alpaca
inner join Farm 
on Alpaca.FarmId = Farm.Id