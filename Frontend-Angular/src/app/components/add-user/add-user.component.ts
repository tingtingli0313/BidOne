import { Component, EventEmitter, Output } from '@angular/core';
import { User } from 'src/app/models/User';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css']
})
export class AddUserComponent {
  @Output() onAddTask: EventEmitter<User> = new EventEmitter();
  @Output() onClearTask: EventEmitter<any> = new EventEmitter();
  firstName: string;
  lastName: string;
  constructor() {
  
  }

  ngOnInit(): void {}
  
  ngOnDestroy() {}

  handleAdd() {
    const newUser: User = {
      firstName: this.firstName,
      lastName: this.lastName,
      id: ""
    };

    this.onAddTask.emit(newUser);
  }

  handleClear() {
    this.firstName = "";
    this.lastName = "";
    this.onClearTask.emit();
  }
}
