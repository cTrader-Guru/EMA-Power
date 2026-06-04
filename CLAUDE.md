# EMA Power — Contesto Strategico

## La Strategia: Terminal A + Terminal B

**Obiettivo:** far perdere Terminal A (demo) in modo controllato e copiare i trade al contrario su Terminal B (reale) tramite Copiix, incassando l'inverso delle perdite di A.

- **Terminal A (demo):** gira EMA Power sul branch `experiment`. È progettato per perdere. Non importa che bruci il conto — è demo.
- **Terminal B (reale):** usa Copiix per copiare ogni trade di A in direzione inversa. Guadagna il Gross movement di A (non il Net, perché le commissioni di A sono affar suo).
- **Simbolo:** BTCUSD — commissioni quasi zero, swap quasi zero.
- **CloseAllAt:** parametro che chiude tutto prima delle 21:00 server (rollover swap). Evita swap su entrambi i terminali.
- **Cap a 3:** Copiix copia la sequenza martingala fino al livello 3 max. Oltre non copia — ma A perde comunque, quindi l'asimmetria resta.

## Branch di Lavoro

Il bot modificato vive nel branch `experiment` (derivato da `master`).

### Modifiche apportate rispetto a `master`
- **Rimosso:** SL/TP in pips, BreakEven, Trailing Stop, relativi parametri e metodi estensione
- **Aggiunto:** SL e TP in denaro (€) gestiti via `position.NetProfit` a ogni tick
- **Aggiunto:** `CloseAllAt` — chiude tutto a una certa ora (es. 20:59) per evitare swap
- **Aggiunto:** statistiche a fine run nei log (`MaxProfit`, `MaxLoss`, in base a `Equity - Balance`)
- **Martingala invariata:** su loss apre direzione opposta con volume × `DMMultiplier`, accumula `CumulativeLoss`, il TP target diventa `CumulativeLoss + TakeProfitMoney`

## Parametri Chiave (branch experiment)

| Parametro | Gruppo | Default | Note |
|---|---|---|---|
| `StopLossMoney` | Strategy | 30 | SL in € su `position.NetProfit` |
| `TakeProfitMoney` | Strategy | 150 | TP base; durante martingala: CumulativeLoss + questo |
| `CloseOnTrigger` | Strategy | false | Chiude su segnale opposto |
| `UseDM` | Strategy | true | Abilita martingala deviation |
| `CloseAllAt` | Pause | 0 | Ora di chiusura forzata (es. 20.59 = 20:59) |

## Logica SL/TP in Codice (OnTick)

```csharp
bool OnSLClose = position.NetProfit <= -StopLossMoney;
bool OnTPClose = position.NetProfit >= (CumulativeLoss + TakeProfitMoney);

if (OnSLClose || OnTPClose)
{
    position.Close();
    continue;
}
```

I check SL/TP sono **fuori** dal blocco `if (!UsingRecovery)` — funzionano anche durante la martingala.

## CloseAllAt (OnTick, inizio)

```csharp
if (CloseAllAt > 0 && Server.Time.ToDouble() >= CloseAllAt)
{
    foreach (var position in Positions.FindAll(MyLabel, SymbolName))
        position.Close();
    return;
}
```

## Statistiche (OnStop)

```csharp
Print("=== STATS ===");
Print("Max Profit : +{0:F2}", MaxProfit);
Print("Max Loss   : -{0:F2}", MaxLoss);
Print("=============");
```

Misurate a ogni tick: `Equity - Balance` (floating P&L delle posizioni aperte).

## Dati Backtest di Riferimento (BTCUSD m5, 0 commissioni)

| | Valore |
|---|---|
| Periodo | 30/09/2022 → 10/12/2022 (~2.5 mesi) |
| Trades | 243 |
| WinRate | 34.2% |
| P&L Terminal A | −2.925 € |
| P&L Terminal B (stima) | ~+2.925 € (con comm. zero) |
| Swap | 0 ✅ |
| Commissioni A | 0 (da verificare su account reale) |

## Note Importanti

- L'ordine di esecuzione usa `ExecuteMarketOrder(..., 0, 0)` — nessun SL/TP broker-side, tutto gestito dal bot
- Su BTCUSD il rollover è ~21:00 ora server — `CloseAllAt` va impostato a 20:50 o prima
- Copiix introduce latenza trascurabile su sequenze brevi (cap 3), non è un problema reale
- La strategia non dipende dalla direzione del mercato: A perde sistematicamente per design, B incassa la specularità
