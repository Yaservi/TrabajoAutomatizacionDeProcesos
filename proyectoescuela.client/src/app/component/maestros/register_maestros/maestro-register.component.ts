import { Component, Input, OnChanges, SimpleChanges, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MaestroService } from '../../../services/maestros/maestro-service';

@Component({
  selector: 'app-maestro-register',
  standalone: true,
  templateUrl: './maestro-register.component.html',
  styleUrl: './maestro-register.component.css',
  imports: [ReactiveFormsModule, CommonModule]
})
export class MaestroRegisterComponent implements OnChanges {
  @Input() maestro: any = null; // Recibe el maestro a editar (o null para nuevo)
  @Output() submitted = new EventEmitter<void>();
  maestroForm: FormGroup;
  mensaje: string = '';
  isEdit: boolean = false;

  constructor(private fb: FormBuilder, private maestroService: MaestroService) {
    this.maestroForm = this.fb.group({
      id: [null], // Para edición
      nombre: ['', Validators.required],
      apellido: ['', Validators.required],
      direccion: ['', Validators.required],
      fechaNacimiento: ['', Validators.required],
      telefono: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      materia: ['', [Validators.required]] 
    });
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['maestro']) {
      if (this.maestro) {
        this.isEdit = true;
        this.maestroForm.patchValue(this.maestro);
      } else {
        this.isEdit = false;
        this.maestroForm.reset();
      }
      this.mensaje = '';
    }
  }

  onSubmit() {
    if (this.maestroForm.valid) {
      if (this.isEdit && this.maestro?.id) {
        // Editar
        this.maestroService.actualizarMaestro(this.maestro.id, this.maestroForm.value).subscribe({
          next: () => {
            this.mensaje = '¡Maestro actualizado exitosamente!';
            this.submitted.emit();
          },
          error: () => {
            this.mensaje = 'Error al actualizar el maestro.';
          }
        });
      } else {
        this.maestroService.registrarMaestro(this.maestroForm.value).subscribe({
          next: () => {
            this.mensaje = '¡Maestro registrado exitosamente!';
            this.maestroForm.reset();
            this.submitted.emit();
          },
          error: () => {
            this.mensaje = 'Error al registrar el maestro.';
          }
        });
      }
    }
  }
}
