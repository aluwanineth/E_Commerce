import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { LocalStorageService } from 'src/app/shared/services/local-storage.service';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private cart = new BehaviorSubject({
    orderId: this.orderId,
    itemCount: this.itemCount
  });

  cartValue = this.cart.asObservable();

  constructor(private storage: LocalStorageService) { }

  get orderId(): string {
    const id = this.storage.getItem('order-id');
    return id ? id : '';
  }

  set orderId(id: string) {
    this.storage.addItem('order-id', id);
    this.cart.next({ orderId: id, itemCount: this.itemCount });
  }

  get itemCount(): number {
    const itemCount = this.storage.getItem('item-count');

    return itemCount ? parseInt(itemCount) : 0;
  }

  set itemCount(item: number) {
    this.storage.addItem('item-count', item.toString());
    this.cart.next({ orderId: this.orderId, itemCount: item });
  }

  incrementItemCount(item: number) {
    this.itemCount = this.itemCount + item;
  }

  decrementItemCount(item: number) {
    this.itemCount = this.itemCount - item;
  }

  clearCart() {
    this.storage.deleteItem('item-count');
    this.cart.next({ orderId: '', itemCount: 0 });
  }
}
