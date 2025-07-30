import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-materia-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './materia-list.component.html',
  styleUrls: ['./materia-list.component.css']
})
export class MateriaListComponent {
  @Input() materias: any[] = [];
  @Output() editarMateria = new EventEmitter<any>();
  @Output() borrarMateria = new EventEmitter<any>();

  editarMateriaHandler(materia: any) {
    this.editarMateria.emit(materia);
  }

  borrarMateriaHandler(materia: any) {
    this.borrarMateria.emit(materia);
  }
}
