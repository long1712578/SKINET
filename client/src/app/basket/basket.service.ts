import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { IBasket } from '../models/basket';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  private url = 'https://localhost:7039/Basket';
  private basketSource = new BehaviorSubject<IBasket>({id: '', items: []});
  public basket$ = this.basketSource.asObservable();
  constructor(private http: HttpClient) { }

  getBasketById(id: string) {
    return this.http.get(`url?id=${id}`)
    .pipe( map((basket: any) => {
      this.basketSource.next(basket)
    })
    );
  };

  setBasket(basket: IBasket) {
    
  }
}
