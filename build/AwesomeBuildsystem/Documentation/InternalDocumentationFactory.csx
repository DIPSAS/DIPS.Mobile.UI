#r "nuget:YamlDotNet, 15.1.2"
#load "models/EventCodes.csx"
#load "models/ErrorCodes.csx"
#load "models/ReleaseNotes.csx"
#load "../Logging/Logger.csx"

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public static class InternalDocumentationFactory
{
    public static async Task<string> CreateEventCodes(string[] eventCodesYamlPaths,  string eventCodesMarkdownPath)
    {
        FileStream fileCreated = null;
        StreamWriter writer = null;
        try
        {
            var markdDownFileName = "event-codes.md";
            var markDownFilePath = Path.Combine(eventCodesMarkdownPath, markdDownFileName);

            fileCreated = File.Create(markDownFilePath);
            fileCreated.Close();
            writer = new StreamWriter(fileCreated.Name, true, Encoding.UTF8);
            await writer.WriteLineAsync($"# Eventkoder");
            await writer.WriteLineAsync($"Dette dokumentet inneholder detaljert informasjon om eventkoder som brukes i Arena Mobil for å logge brukeradferd. Her finner du en oversikt over de ulike eventkodene, hva de representerer, og hvordan de benyttes. For hver eventkode er det spesifisert en beskrivelse, fra hvilken versjon koden er tilgjengelig, samt hvilke tilleggsdata (parametere) som sendes med.");
            await writer.WriteLineAsync($"## Informasjon");
            await writer.WriteLineAsync($"> Sist oppdatert: {DateTime.Now.ToString("dd/MM/yyyy")}\n");
            await writer.WriteLineAsync($"> YA : Eventkode for Arena Mobil \n");
            await writer.WriteLineAsync($"> YC : Eventkode for DIPS.Mobile.Essentials. *(Brukes av Arena Mobil)* \n");

            foreach (var eventCodesYamlPath in eventCodesYamlPaths)
            {
                if (!File.Exists(eventCodesYamlPath)) continue;

                var yml = await File.ReadAllTextAsync(eventCodesYamlPath);
                var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

                //yml contains a string containing your YAML
                var root = deserializer.Deserialize<Root>(yml);
                if (root == null) return null;
                await WriteEventCodes(writer, root.EventCodes);
            }

            await writer.WriteLineAsync($"");
            await writer.WriteLineAsync($"___");
            await writer.WriteLineAsync($"Copyright (c) {DateTime.Now.Year} DIPS AS");
            writer.Close();

            return markDownFilePath;
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            if(fileCreated != null)
            {
                fileCreated.Close();
            }
            
            if(writer != null)
            {
                writer.Close();
            }
            
        }
    }

    public static async Task<string> CreateErrorCodes(string[] errorCodesYamlPaths,  string errorCodesCodesMarkdownPath)
    {
        FileStream fileCreated = null;
        StreamWriter writer = null;
        try
        {
            var markdDownFileName = "error-codes.md";
            var markDownFilePath = Path.Combine(errorCodesCodesMarkdownPath, markdDownFileName);

            fileCreated = File.Create(markDownFilePath);
            fileCreated.Close();
            writer = new StreamWriter(fileCreated.Name, true, Encoding.UTF8);
            await writer.WriteLineAsync($"# Feilkoder");
            await writer.WriteLineAsync($"Dette dokumentet inneholder en oversikt over feilkoder brukt i Arena Mobil. Hver feilkode har et unikt nummer, en kort beskrivelse, en mer detaljert beskrivelse, versjonen den ble introdusert i. Feilkodene hjelper med å identifisere spesifikke problemer og feil som kan oppstå i applikasjonen, og gir veiledning for hvordan disse kan håndteres eller løses. Dette er viktig for å sikre en effektiv feilsøking og vedlikehold av Arena Mobil.");
            await writer.WriteLineAsync($"## Informasjon");
            await writer.WriteLineAsync($"\n> Sist oppdatert: {DateTime.Now.ToString("dd/MM/yyyy")}\n");
            await writer.WriteLineAsync($"> YA : Feilkode for Arena Mobil \n");
            await writer.WriteLineAsync($"> YC : Feilkode for DIPS.Mobile.Essentials. *(Brukes av Arena Mobil)* \n");

            foreach (var eventCodesYamlPath in errorCodesYamlPaths)
            {
                if (!File.Exists(eventCodesYamlPath)) continue;

                var yml = await File.ReadAllTextAsync(eventCodesYamlPath);
                var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

                //yml contains a string containing your YAML
                var root = deserializer.Deserialize<Root>(yml);
                if (root == null) return null;
                await WriteEventCodes(writer, root.ErrorCodes);
            }

            await writer.WriteLineAsync($"");
            await writer.WriteLineAsync($"___");
            await writer.WriteLineAsync($"Copyright (c) {DateTime.Now.Year} DIPS AS");
            writer.Close();

            return markDownFilePath;
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            if(fileCreated != null)
            {
                fileCreated.Close();
            }
            
            if(writer != null)
            {
                writer.Close();
            }
            
        }
    }

    private static async Task WriteEventCodes(StreamWriter writer, EventCode[] eventCodes)
    {
        foreach (var eventCode in eventCodes)
        {
            await writer.WriteLineAsync($"## {eventCode.Code} - {eventCode.ShortName}");

            if (eventCode.Deprecated == null && eventCode.FromVersion != null)
            {
                await writer.WriteLineAsync($"> Lagt til i versjon {eventCode.FromVersion}");
            }

            if (eventCode.Deprecated != null)
            {
                var deprecatedText = $"> 🔺 Fjernet i versjon {eventCode.Deprecated.FromVersion}";
                if (eventCode.Deprecated.ReplacedWith != null)
                {
                    deprecatedText += $", erstattet med ";
                    foreach (var replacedWithVersion in eventCode.Deprecated.ReplacedWith)
                    {
                        deprecatedText += $"{replacedWithVersion}";
                        if (eventCode.Deprecated.ReplacedWith.LastOrDefault() != replacedWithVersion)
                        {
                            deprecatedText += ", ";
                        }

                    }
                }
                await writer.WriteLineAsync(deprecatedText += "\n");
            }

            await writer.WriteLineAsync($"### Beskrivelse");
            await writer.WriteLineAsync($"{eventCode.Description}");
            
            
            if(eventCode is ErrorCode errorCode)
            {
                if(!string.IsNullOrEmpty(errorCode.Solution))
                {
                    await writer.WriteLineAsync($"### Løsning");
                    await writer.WriteLineAsync($"{errorCode.Solution}");
                }
            }

            if (eventCode.Parameters != null && eventCode.Parameters.Any())
            {
                await writer.WriteLineAsync($"### Parametere");
                foreach (var parameter in eventCode.Parameters)
                {
                    await writer.WriteLineAsync($"- {parameter}");
                }
            }
        }
    }

    public static async Task<string> CreateReleaseNotes(string releaseNotesYamlPath, string releaseNotesMarkdownPath)
    {

        FileStream fileCreated = null;
        StreamWriter writer = null;
        try
        {
            var yml = await File.ReadAllTextAsync(releaseNotesYamlPath);
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            //yml contains a string containing your YAML
            var product = deserializer.Deserialize<Product>(yml);
            if (product == null) return null;

            var fileInfo = new FileInfo(releaseNotesYamlPath);
            var markdDownFileName = fileInfo.Name.Replace(fileInfo.Extension, ".md");
            var markDownFilePath = Path.Combine(releaseNotesMarkdownPath, markdDownFileName);
            fileCreated = File.Create(markDownFilePath);
            fileCreated.Close();
            writer = new StreamWriter(fileCreated.Name, true, Encoding.UTF8);
            await writer.WriteLineAsync($"# Versjonsdokumentasjon");
            await writer.WriteLineAsync($"Dette dokumentet inneholder detaljert informasjon om de nyeste oppdateringene av Arena Mobil, organisert etter versjon. Her finner du en oversikt over nyheter, feilrettinger og endringer for hver versjon. For hver oppføring kan det være nødvendige handlinger som forvaltere bør være oppmerksomme på, samt tilknytninger til relevante lenker for mer informasjon.");
            await writer.WriteLineAsync($"\n> Sist oppdatert: {DateTime.Now.ToString("dd/MM/yyy")}\n");
            foreach (var version in product.Versions)
            {

                await writer.WriteLineAsync($"## {version.Version}");

                if(!version.HasChanges){
                    await writer.WriteLineAsync($"*Ingen nyheter, feilrettinger eller endringer er lagt til versjonen.*");   
                }else
                {
                    var changes = version.ReleaseNotes.OrderBy(r => r.Type);
                    foreach (var change in changes)
                    {
                        if(change.Skip) continue; //Should not be printed to the mark down file.
                        
                        var typeDisplayName = "";
                        switch(change.Type)
                        {
                            case(ReleaseType.Unknown):
                                throw new Exception($"You need to specify a type: for change with title '{change.Title}'. Choose one of the following: {ReleaseType.Change.ToString().ToLower()}, {ReleaseType.News.ToString().ToLower()}, {ReleaseType.BugFix.ToString().ToLower()}");
                            case(ReleaseType.Change):
                                typeDisplayName = "Endring";
                                break;
                            case (ReleaseType.News):
                                typeDisplayName = "Nyhet";
                                break;
                            case (ReleaseType.BugFix):
                                typeDisplayName = "Feilretting";
                                break;
                        }
                        await writer.WriteLineAsync($"\n### {typeDisplayName} : {change.Title}");
                        await writer.WriteLineAsync($"{change.Description}");

                        if(change.RequiredActions.Count > 0)
                        {
                            await writer.WriteLineAsync($"\n#### Nødvendige handlinger");
                            foreach (var requiredAction in change.RequiredActions)
                            {
                                await writer.WriteLineAsync($"- {requiredAction}");
                            }
                        }

                        if(change.HasAttachements)
                        {
                            await writer.WriteLineAsync($"\n#### Tilknytninger");
                            foreach(var dsak in change.Dsak)
                            {
                                var url = $"https://online2.superoffice.com/Cust25129/CS/scripts/ticket.fcgi?_sf=0&action=doScreenDefinition&idString=viewTicket_80_v2&entryId={dsak}";
                                await writer.WriteLineAsync($" - [dsak: {dsak}]({url})");
                            }
                        }

                        await writer.WriteAsync("\n___\n");
                    }
                }
            }

                
            await writer.WriteLineAsync($"");
            await writer.WriteLineAsync($"Copyright (c) {DateTime.Now.Year} DIPS AS");
            writer.Close();
            return markDownFilePath;
        }catch(Exception e)
        {
            Logger.LogError("Something went wrong when creating release notes:, exception message: " + e.Message, true);
            throw;
        }
        finally
        {
            fileCreated.Close();
            writer.Close();
        }

    }
}