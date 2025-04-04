namespace eCommerce.Core.Dto;

/* Utilizzo di record. Sono immutabili per default.
*  Questo significa che una volta creato un oggetto di tipo record, i suoi campi non possono essere modificati
*  I record possono essere dichiarati con parametri nel costruttore, che sono automaticamente trasformati in proprietà */

public record LoginRequest(
    string? Email, 
    string? Password);
