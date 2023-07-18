import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { User } from 'src/app/models/User';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
    @Input() user: User;
    @Output() onClickReminder: EventEmitter<User> = new EventEmitter();

    constructor() {}

    ngOnInit(): void {}
    
    handleMarkAsComplete(task) {
      this.onClickReminder.emit(task);
    }
}