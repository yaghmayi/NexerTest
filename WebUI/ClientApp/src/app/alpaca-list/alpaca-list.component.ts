import { Component } from '@angular/core';
import { AlpacaService } from "../services/alpaca.service";
import { IAlpaca } from "../interfaces/alpaca.interface";
import { ISummaryRow } from "../interfaces/summaryRow";

@Component({
  selector: 'alpaca-list',
  templateUrl: './alpaca-list.component.html',
})

export class AlpacaListComponent {
  alpacaItems: IAlpaca[] = [];
  summaryRows: ISummaryRow[] = [];

  constructor(private readonly service: AlpacaService) {
  }

  ngOnInit() {
    this.loadAlpacaItems();
  }

  private loadAlpacaItems() {
    this.service.getAlpacaList().subscribe(result => { this.alpacaItems = result; });
  }

  isThereSelectedAlpaca(): boolean {
    const findIndex = this.alpacaItems.findIndex(x => x.isSelected);

    return findIndex !== -1;
  }

  refreshGroupItems() {
    this.summaryRows = [];

    let totalCost = 0;

    this.alpacaItems.forEach(alpacaItem => {

      if (alpacaItem.isSelected) {
        totalCost += alpacaItem.cost;

        let row : ISummaryRow = this.summaryRows.find(x => x.farmName === alpacaItem.farm.name);

        if (row != null) {
          row.alpacasCount++;
          row.cost += alpacaItem.cost;
        } else {
          row = {};
          row.farmName = alpacaItem.farm.name;
          row.alpacasCount = 1;
          row.cost = alpacaItem.cost;

          this.summaryRows.push(row);
        }
      }
    });

    let totalRow: ISummaryRow = {
       cost: totalCost,
       isTotalRow: true
    };

    this.summaryRows.push(totalRow);
  }
}
