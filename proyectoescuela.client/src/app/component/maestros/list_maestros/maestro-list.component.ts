import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-maestro-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './maestro-list.component.html',
  styleUrl: './maestro-list.component.css'
})
export class MaestroListComponent {
  @Input() maestros: any[] = [];
  @Output() editarMaestro = new EventEmitter<any>();
  @Output() borrarMaestro = new EventEmitter<any>();


  editarMaestroHandler(alumno: any) {
    this.editarMaestro.emit(alumno);
  }

  borrarMaestroHandler(alumno: any) {
    this.borrarMaestro.emit(alumno);
  }
}
