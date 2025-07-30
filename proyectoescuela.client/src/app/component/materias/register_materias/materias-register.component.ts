import { Component, Input, OnChanges, SimpleChanges, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MateriaService } from '../../../services/materias/materia-service';

@Component({
  selector: 'app-materia-register',
  standalone: true,
  templateUrl: './materia-register.component.html',
  styleUrls: ['./materia-register.component.css'],
  imports: [ReactiveFormsModule, CommonModule]
})
export class MateriaRegisterComponent implements OnChanges {
  @Input() materia: any = null; // Recibe la materia a editar o null para nueva
  @Input() maestros: any[] = []; // Lista de maestros para el select
  @Output() submitted = new EventEmitter<void>();

  materiaForm: FormGroup;
  mensaje: string = '';
  isEdit: boolean = false;

  constructor(private fb: FormBuilder, private materiaService: MateriaService) {
    this.materiaForm = this.fb.group({
      id: [null], // Para edición
      nombreMateria: ['', Validators.required],
      descripcion: ['', Validators.required],
      maestroId: ['', Validators.required]
    });
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['materia']) {
      if (this.materia) {
        this.isEdit = true;
        this.materiaForm.patchValue(this.materia);
      } else {
        this.isEdit = false;
        this.materiaForm.reset();
      }
      this.mensaje = '';
    }
  }

  onSubmit() {
    if (this.materiaForm.valid) {
      if (this.isEdit && this.materia?.id) {
        // Editar
        this.materiaService.actualizarMateria(this.materia.id, this.materiaForm.value).subscribe({
          next: () => {
            this.mensaje = '¡Materia actualizada exitosamente!';
            this.submitted.emit();
          },
          error: () => {
            this.mensaje = 'Error al actualizar la materia.';
          }
        });
      } else {
        // Registrar
        this.materiaService.crearMateria(this.materiaForm.value).subscribe({
          next: () => {
            this.mensaje = '¡Materia registrada exitosamente!';
            this.materiaForm.reset();
            this.submitted.emit();
          },
          error: () => {
            this.mensaje = 'Error al registrar la materia.';
          }
        });
      }
    }
  }
}
