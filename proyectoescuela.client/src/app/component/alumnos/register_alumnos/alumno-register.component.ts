import { Component, Input, OnChanges, SimpleChanges, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, AbstractControl, ValidationErrors, FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AlumnoService } from '../../../services/alumnos/alumno-service';

// Custom validators
export function noNumbersValidator(): ValidationErrors | null {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;
    if (!value) {
      return null;
    }
    const hasNumbers = /\d/.test(value);
    return hasNumbers ? { containsNumbers: true } : null;
  };
}

export function noRepeatedLettersValidator(maxRepetitions: number = 4): ValidationErrors | null {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;
    if (!value) {
      return null;
    }
    const repeatedLettersPattern = new RegExp(`(.)\\1{${maxRepetitions},}`, 'i');
    const hasRepeatedLetters = repeatedLettersPattern.test(value);
    return hasRepeatedLetters ? { repeatedLetters: { maxRepetitions } } : null;
  };
}

export function onlyNumbersValidator(): ValidationErrors | null {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;
    if (!value) {
      return null;
    }
    // Remove the prefix if it exists (we'll handle it separately in the UI)
    const phoneNumber = value.toString().replace(/^\+\d+\s/, '');
    const hasNonNumbers = /[^\d]/.test(phoneNumber);
    return hasNonNumbers ? { containsNonNumbers: true } : null;
  };
}

export function dateMinimumYearsAgoValidator(years: number = 5): ValidationErrors | null {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;
    if (!value) {
      return null;
    }
    const selectedDate = new Date(value);
    const today = new Date();
    const minimumDate = new Date();
    minimumDate.setFullYear(today.getFullYear() - years);

    return selectedDate > minimumDate ? { dateNotOldEnough: { requiredYears: years } } : null;
  };
}

@Component({
  selector: 'app-alumno-register',
  standalone: true,
  templateUrl: './alumno-register.component.html',
  styleUrls: ['./alumno-register.component.css'],
  imports: [ReactiveFormsModule, FormsModule, CommonModule]
})
export class AlumnoRegisterComponent implements OnChanges {
  @Input() alumno: any = null; // Recibe el alumno a editar (o null para nuevo)
  @Output() submitted = new EventEmitter<void>();
  alumnoForm: FormGroup;
  mensaje: string = '';
  isEdit: boolean = false;
  prefijos: string[] = ['+1', '+52', '+34', '+57', '+58', '+51', '+56', '+54', '+55', '+593'];
  prefijoSeleccionado: string = '+52'; // Default to Mexico

  constructor(private fb: FormBuilder, private alumnoService: AlumnoService) {
    this.alumnoForm = this.fb.group({
      id: [null], // Para edición
      nombre: ['', [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(50),
        noNumbersValidator(),
        noRepeatedLettersValidator(4)
      ]],
      apellido: ['', [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(50),
        noNumbersValidator(),
        noRepeatedLettersValidator(4)
      ]],
      direccion: ['', [
        Validators.required,
        Validators.minLength(5),
        Validators.maxLength(100)
      ]],
      fechaNacimiento: ['', [
        Validators.required,
        dateMinimumYearsAgoValidator(5)
      ]],
      telefono: ['', [
        Validators.required,
        Validators.minLength(7),
        Validators.maxLength(15),
        onlyNumbersValidator()
      ]],
      email: ['', [
        Validators.required,
        Validators.email,
        Validators.maxLength(100)
      ]]
    });
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['alumno']) {
      if (this.alumno) {
        this.isEdit = true;
        this.alumnoForm.patchValue(this.alumno);
      } else {
        this.isEdit = false;
        this.alumnoForm.reset();
      }
      this.mensaje = '';
    }
  }

  onSubmit() {
    if (this.alumnoForm.valid) {
      if (this.isEdit && this.alumno?.id) {
        // Editar
        this.alumnoService.actualizarAlumno(this.alumno.id, this.alumnoForm.value).subscribe({
          next: () => {
            this.mensaje = '¡Alumno actualizado exitosamente!';
            this.submitted.emit();
          },
          error: () => {
            this.mensaje = 'Error al actualizar el alumno.';
          }
        });
      } else {
        this.alumnoService.registrarAlumno(this.alumnoForm.value).subscribe({
          next: () => {
            this.mensaje = '¡Alumno registrado exitosamente!';
            this.alumnoForm.reset();
            this.submitted.emit();
          },
          error: () => {
            this.mensaje = 'Error al registrar el alumno.';
          }
        });
      }
    }
  }
}
