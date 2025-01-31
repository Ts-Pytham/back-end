﻿namespace DentallApp.Features.Chatbot.Factories;

public class AdaptiveCardFactory
{
    public static PromptOptions CreateSingleChoiceCard(string cardJson, List<AdaptiveChoice> choices)
    {
        var card = AdaptiveCard.FromJson(cardJson).Card;
        var choiceSetInput = card.Body[1] as AdaptiveChoiceSetInput;
        choiceSetInput.Choices = choices;
        return PromptOptionsFactory.Create(
            AttachmentFactory.Create(card.ToJson(), AdaptiveCard.ContentType), 
            choiceSetInput.ErrorMessage
        );
    }

    public static PromptOptions CreateDateCard(string cardJson)
    {
        var currentDate = DateTime.Now.Date;
        double value = EnvReader.Instance.GetDoubleValue(AppSettings.MaxDateInDateInput);
        var maxDate = currentDate.AddDays(value);
        var card = AdaptiveCard.FromJson(cardJson).Card;
        var dateInput = card.Body[1] as AdaptiveDateInput;
        dateInput.Min = currentDate.ToString("yyyy-MM-dd");
        dateInput.Max = maxDate.ToString("yyyy-MM-dd");
        return PromptOptionsFactory.Create(
            AttachmentFactory.Create(card.ToJson(), AdaptiveCard.ContentType), 
            dateInput.ErrorMessage
        );
    }
}
