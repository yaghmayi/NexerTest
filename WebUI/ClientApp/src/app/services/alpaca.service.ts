import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { IAlpaca } from "../interfaces/alpaca.interface";
import { HttpHelpersService } from "./http-helpers.service";

@Injectable()
export class AlpacaService {

  private alpacaApiAddress: string;

  constructor(private readonly http: HttpClient, private readonly httpHelpers: HttpHelpersService) {
    this.alpacaApiAddress = this.httpHelpers.getApiBaseUrl() + "alpaca/";
  }

  getAlpacaList() {
    return this.http.get<IAlpaca[]>(this.alpacaApiAddress + "list");
  }

  getAlpaca(id: number) {
    return this.http.get<IAlpaca>(this.alpacaApiAddress + "getById/" + id, this.httpHelpers.getHttpOptions());
  }

  saveAlpaca(alpaca : IAlpaca) {
    return this.http.post(this.alpacaApiAddress + "add", JSON.stringify(alpaca), this.httpHelpers.getHttpOptions());
  }

  deleteAlpaca(id: number) {
    return this.http.delete(this.alpacaApiAddress + "delete/" + id, this.httpHelpers.getHttpOptions());
  }

  getFarmList() {
    return this.http.get<IAlpaca[]>(this.alpacaApiAddress + "farms");
  }

  
}
